using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Sample.Business.Model;
using WF.Sample.Business.Workflow;
using WF.Sample.Helpers;
using WF.Sample.Models;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WF.Sample.Business.DataAccess;
using OptimaJet.Workflow.Core.Persistence;
using OptimaJet.Workflow.Core.Runtime;
using WF.Sample.Extensions;
using VLWorkflowRuntime.WorkflowInterface;
using VLWorkflowRuntime.WorkflowModels;

namespace WF.Sample.Controllers
{
    public class TravelRequestController : Controller
    {
        private readonly ITravelRequestRepository _TravelRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWorkflowRepository _workFlowRepository;
        private readonly IMapper _mapper;
        private int pageSize = 15;
        public TravelRequestController(ITravelRequestRepository TravelRequestRepository, IEmployeeRepository employeeRepository, IMapper mapper, IWorkflowRepository workFlowRepository)
        {
            _TravelRequestRepository = TravelRequestRepository;
            _employeeRepository = employeeRepository;
            _workFlowRepository = workFlowRepository;
            _mapper = mapper;
        }

        #region Index
        public ActionResult Index(int page = 1)
        {
            int count = 0;
            var identityId = CurrentUserSettings.GetCurrentUser(HttpContext);
            var user = _employeeRepository.GetById(identityId);
            //ViewBag.UserRole = user.GetListRoles();
            return View(new TravelRequestIndexModel<TravelRequestModel>
            {
                TravelRequestListModel = new TravelRequestListModel<TravelRequestModel>()
                {
                    Page = page,
                    PageSize = pageSize,
                    Docs = _TravelRequestRepository.Get(out count, page, pageSize).Select(GetTravelRequestModel<TravelRequestModel>).ToList(),
                    Count = count,
                },
                UserRole = user.GetListRoles()
            });
        }

        public async Task<ActionResult> Inbox(int page = 1)
        {
            var identityId = CurrentUserSettings.GetCurrentUser(HttpContext).ToString();
            
            var inbox = await WorkflowInit.Runtime.PersistenceProvider
                .GetInboxByIdentityIdAsync(identityId, Paging.Create(page, pageSize));
            
            int count = await WorkflowInit.Runtime.PersistenceProvider.GetInboxCountByIdentityIdAsync(identityId);

            return View("Inbox", new TravelRequestListModel<InboxTravelRequestModel>()
            {
                Page = page,
                PageSize = pageSize,
                Docs = GetTravelRequestsByInbox(inbox),
                Count = count,
            });
        }

        public async Task<ActionResult> Outbox(int page = 1)
        {
            var identityId = CurrentUserSettings.GetCurrentUser(HttpContext).ToString();
            
            var outbox = await WorkflowInit.Runtime.PersistenceProvider
                .GetOutboxByIdentityIdAsync(identityId, Paging.Create(page, pageSize));
            
            int count = await WorkflowInit.Runtime.PersistenceProvider.GetOutboxCountByIdentityIdAsync(identityId);
            
            return View("Outbox", new TravelRequestListModel<OutboxTravelRequestModel>()
            {
                Page = page,
                Docs =  GetTravelRequestsByOutbox(outbox),
                PageSize = pageSize,
                Count = count,
            });
        }
        
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(Guid? Id)
        {
            TravelRequestIndexModel<TravelRequestModel> model = null;

            if(Id.HasValue)
            {
                var d = _TravelRequestRepository.Get(Id.Value);
                if(d != null)
                {
                    await CreateWorkflowIfNotExists(Id.Value, d.WorkflowSchemeCode);
                    var history = await GetApprovalHistory(Id.Value);
                    
                    model = new TravelRequestIndexModel<TravelRequestModel>()
                    {
                        TravelRequestListModel = new TravelRequestListModel<TravelRequestModel>
                        {
                            Docs = new List<TravelRequestModel> {
                                 new TravelRequestModel()
                                {
                                    Id = d.Id,
                                    AuthorId = d.AuthorId,
                                    AuthorName = d.Author.Name,
                                    Comment = d.Comment,
                                    ManagerId = d.ManagerId,
                                    ManagerName =
                                        d.ManagerId.HasValue ? d.Manager.Name : string.Empty,
                                    TravelRequestNumber = d.TravelRequestNumber,
                                    Number = d.Number,
                                    StateName = d.StateName,
                                    TotalCost = d.TotalCost,
                                    Commands = await GetCommands(Id.Value),
                                    AvailiableStates = await GetStates(Id.Value),
                                    HistoryModel = new TravelRequestHistoryModel{Items = history},
                                    WorkflowSchemeCode = d.WorkflowSchemeCode,
                                } }
                        }
                    };
                }
                
            }
            else
            {
                Guid userId = CurrentUserSettings.GetCurrentUser(HttpContext);
                var trnumber = GenerateTRNumber("TS");

                ProcessActivity initialActivity = _workFlowRepository.GetInitialActivityBySchemaCode("CHC");
                model = new TravelRequestIndexModel<TravelRequestModel>() { TravelRequestListModel = new TravelRequestListModel<TravelRequestModel>
                {
                    Docs = new List<TravelRequestModel> {
                    new TravelRequestModel(initialActivity?.Name)
                        {
                            AuthorId = userId,
                            AuthorName = _employeeRepository.GetNameById(userId),
                            TravelRequestNumber = trnumber
                        }}
                }
                };
            }

            return View(model);
        }
        
        private string GenerateTRNumber(string corporateId)
        {
            Random random = new Random();
            // Generate random 3-letter string
            string randomLetters = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            // Generate random 2-digit string
            string randomDigits = new string(Enumerable.Repeat("0123456789", 2)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            var trnumber = corporateId + DateTime.UtcNow.Year % 100 + randomLetters + randomDigits;
            if(_TravelRequestRepository.GetByTrNumber(trnumber) != null)
            {
                trnumber = GenerateTRNumber(corporateId);
            }

            return trnumber;
        }
        public async Task<ActionResult> ExecuteCommand(Guid Id, TravelRequestModel model, string command)
        {
            await ExecuteCommand(Id, command, model);
            return RedirectToAction("Inbox");
            
        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(Guid? Id, TravelRequestModel model, string button)
        {
         
            if (!ModelState.IsValid)
            {
                return View(new TravelRequestIndexModel<TravelRequestModel>
                {
                    TravelRequestListModel = new TravelRequestListModel<TravelRequestModel>()
                    {
                        Docs = new List<TravelRequestModel>() { model},
                    }
                });
            }

            TravelRequest doc = _mapper.Map<TravelRequest>(model);

            try
            {
                doc = _TravelRequestRepository.InsertOrUpdate(doc);

                if (doc == null)
                {
                    ModelState.AddModelError("", "Row not found!");
                    return View(new TravelRequestIndexModel<TravelRequestModel>
                    {
                        TravelRequestListModel = new TravelRequestListModel<TravelRequestModel>()
                        {
                            Docs = new List<TravelRequestModel>() { model },
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder("Save error. " + ex.Message);
                if (ex.InnerException != null)
                    sb.AppendLine(ex.InnerException.Message);
                ModelState.AddModelError("", sb.ToString());
                return View(model);
            }

            if (button == "SaveAndExit")
                return RedirectToAction("Index");
            if (button != "Save")
            {
                await ExecuteCommand(doc.Id, button, model);
            }
            return RedirectToAction("Edit", new { doc.Id});
            
        }

        #endregion

        #region Delete
        
        public async Task<ActionResult> DeleteRows(Guid[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Content("Items not selected");

            try
            {
                foreach (var id in ids)
                {
                    await WorkflowInit.Runtime.PersistenceProvider.DeleteProcessAsync(id);
                }
                
                _TravelRequestRepository.Delete(ids);
                
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return Content("Rows deleted");
        }
        #endregion

        #region Workflow
        private async Task<TravelRequestCommandModel[]> GetCommands(Guid id)
        {
            var result = new List<TravelRequestCommandModel>();
            var tr = _TravelRequestRepository.Get(id);
            ProcessCommand[] commands = _workFlowRepository.GetAvailableCommands(tr.WorkflowSchemeCode, id, CurrentUserSettings.GetCurrentUser(HttpContext).ToString());
            //var commands = WorkflowInit.Runtime.GetAvailableCommands(id, CurrentUserSettings.GetCurrentUser(HttpContext).ToString());
            foreach (var workflowCommand in commands)
            {
                if (result.Count(c => c.key == workflowCommand.Name) == 0)
                    result.Add(new TravelRequestCommandModel() { key = workflowCommand.Name, value = workflowCommand.Name });
            }
            return result.ToArray();
        }

        private async Task<Dictionary<string, string>> GetStates(Guid id)
        {

            var result = new Dictionary<string, string>();
            var tr = _TravelRequestRepository.Get(id);
            ProcessActivity[] activities = _workFlowRepository.GetAvailableActivities(tr.WorkflowSchemeCode, id, CurrentUserSettings.GetCurrentUser(HttpContext).ToString());
            //var states = await WorkflowInit.Runtime.GetAvailableStateToSetAsync(id);
            foreach (var state in activities)
            {
                if (!result.ContainsKey(state.Name))
                    result.Add(state.State, state.Name);
            }
            return result;

        }

        private async Task ExecuteCommand(Guid id, string commandName, TravelRequestModel TravelRequest)
        {
            var currentUser = CurrentUserSettings.GetCurrentUser(HttpContext).ToString();
            
            if (commandName.Equals("SetState", StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(TravelRequest.StateNameToSet))
                    return;
                
                var setStateParams = new SetStateParams(id,TravelRequest.StateNameToSet)
                {
                    IdentityId = currentUser,
                    ImpersonatedIdentityId = currentUser
                }.AddTemporaryParameter("Comment",TravelRequest.Comment);
                
                await WorkflowInit.Runtime.SetStateAsync(setStateParams);
              
                return;
            }
            var tr = _TravelRequestRepository.Get(id);
            if (commandName.Equals("Resume", StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(TravelRequest.StateNameToSet))
                    return;
                //var pi = await WorkflowInit.Runtime.GetProcessInstanceAndFillProcessParametersAsync(id);
                //var activity = pi.ProcessScheme.Activities.FirstOrDefault(a => a.IsForSetState && a.State.Equals(TravelRequest.StateNameToSet, StringComparison.OrdinalIgnoreCase));


                
                var scheme = _workFlowRepository.GetProcessBySchemeCode(tr.WorkflowSchemeCode);
                //var pi = await WorkflowInit.Runtime.GetProcessInstanceAndFillProcessParametersAsync(id);
                var activity = scheme.Activities.FirstOrDefault(a => a.IsForSetState && a.State.Equals(TravelRequest.StateNameToSet, StringComparison.OrdinalIgnoreCase));

                if (activity == null)
                {
                    return;
                }
                var resumeParams = new ResumeParams(id,activity.Name)
                {
                    IdentityId = currentUser,
                    ImpersonatedIdentityId = currentUser
                }.AddTemporaryParameter("Comment",TravelRequest.Comment);
                
                await WorkflowInit.Runtime.ResumeAsync(resumeParams);
                return;
            }

            var commands = await WorkflowInit.Runtime.GetAvailableCommandsAsync(id, currentUser);
            //var commands = _workFlowRepository.GetAvailableCommands(TravelRequest.WorkflowSchemeCode, id, currentUser);
            var command =
                commands.FirstOrDefault(
                    c => c.CommandName.Equals(commandName, StringComparison.CurrentCultureIgnoreCase));
            
            if (command == null)
                return;

            //if (command.Parameters.Count(p => p.ParameterName == "Comment") == 1)
            //    command.Parameters.Single(p => p.ParameterName == "Comment").Value = TravelRequest.Comment ?? string.Empty;

            //await WorkflowInit.Runtime.ExecuteCommandAsync(command,currentUser,currentUser);
            //var tr = _TravelRequestRepository.Get(id);



            await _TravelRequestRepository.ExecuteCommandAsync<MsSql.TravelRequest>(command.CommandName, tr.Id, currentUser, tr.WorkflowSchemeCode);
        }

        private async Task CreateWorkflowIfNotExists(Guid id, string workflowSchemeCode = "TravelRequestScheme")
        {
            if (await WorkflowInit.Runtime.IsProcessExistsAsync(id))
                return;

            await WorkflowInit.Runtime.CreateInstanceAsync(workflowSchemeCode, id);
        }

        #endregion

        private TDoc GetTravelRequestModel<TDoc>(TravelRequest d)
            where TDoc:TravelRequestModel, new()
        {
            return new TDoc()
            {
                Id = d.Id,
                AuthorId = d.AuthorId,
                AuthorName = d.Author.Name,
                Comment = d.Comment,
                ManagerId = d.ManagerId,
                ManagerName = d.ManagerId.HasValue ? d.Manager.Name : string.Empty,
                TravelRequestNumber = d.TravelRequestNumber,
                Number = d.Number,
                StateName = d.StateName,
                TotalCost = d.TotalCost
            };
        }

        private List<InboxTravelRequestModel> GetTravelRequestsByInbox(List<InboxItem> inbox)
        {
            var ids = inbox.Select(x => x.ProcessId).Distinct().ToList();
            
            var TravelRequests = _TravelRequestRepository.GetByIds(ids)
                .ToDictionary(x=>x.Id, x=>x);
            
            var docs = new List<InboxTravelRequestModel>();
            
            foreach (var inboxItem in inbox)
            {
                InboxTravelRequestModel doc;
                
                //if TravelRequest is exists
                if (TravelRequests.TryGetValue(inboxItem.ProcessId, out TravelRequest _doc))
                {
                    doc = GetTravelRequestModel<InboxTravelRequestModel>(_doc);
                }
                else
                {
                    doc = new InboxTravelRequestModel();
                    doc.Id = inboxItem.ProcessId;
                    doc.IsCorrect = false;
                    doc.StateName = TravelRequestModel.NotFoundError;
                }

                doc.AvailableCommands = inboxItem.AvailableCommands;
                doc.AddingDate = inboxItem.AddingDate.ToString();
                docs.Add(doc);
            }

            return docs;
        }
        
        private List<OutboxTravelRequestModel> GetTravelRequestsByOutbox(List<OutboxItem> outbox)
        {
            var ids = outbox.Select(x => x.ProcessId).Distinct().ToList();
            
            var TravelRequests = _TravelRequestRepository.GetByIds(ids)
                .ToDictionary(x=>x.Id, x=>x);
            
            var docs = new List<OutboxTravelRequestModel>();
            
            foreach (var outboxItem in outbox)
            {
                OutboxTravelRequestModel doc;
                
                //if TravelRequest is exists
                if (TravelRequests.TryGetValue(outboxItem.ProcessId, out TravelRequest _doc))
                {
                    doc = GetTravelRequestModel<OutboxTravelRequestModel>(_doc);
                }
                else
                {
                    doc = new OutboxTravelRequestModel();
                    doc.Id = outboxItem.ProcessId;
                    doc.IsCorrect = false;
                    doc.StateName = TravelRequestModel.NotFoundError;
                }

                doc.ApprovalCount = outboxItem.ApprovalCount;
                doc.FirstApprovalTime = outboxItem.FirstApprovalTime;
                doc.LastApprovalTime = outboxItem.LastApprovalTime;
                doc.LastApproval = outboxItem.LastApproval;
                docs.Add(doc);
            }

            return docs;
        }
        
        private async Task<List<TravelRequestApprovalHistory>> GetApprovalHistory(Guid id)
        {
            var approvalHistory = await WorkflowInit.Runtime.PersistenceProvider
                .GetApprovalHistoryByProcessIdAsync(id);
            var employees =  _employeeRepository.GetAll();
            List<TravelRequestApprovalHistory> histories = new List<TravelRequestApprovalHistory>();
            foreach (var item in approvalHistory)
            {
                Employee employee = null;
                if (Guid.TryParse(item.IdentityId,  out id) )
                {
                    employee = employees.FirstOrDefault(x => x.Id == id);
                }

                var allowedTo = new List<string>();
                foreach (var user in item.AllowedTo)
                {
                    //Get name if it's Guid
                    if(Guid.TryParse(user, out Guid guid))
                    {
                        allowedTo.Add(employees.FirstOrDefault(x => x.Id == guid)?.Name??user);
                    }
                    else
                    {
                        allowedTo.Add(user);
                    }
                }
                
                histories.Add(new TravelRequestApprovalHistory()
                {
                    Id = item.Id,
                    ProcessId = item.ProcessId,
                    IdentityId  = item.IdentityId,
                    AllowedTo  = string.Join(",", allowedTo),
                    TransitionTime  = item.TransitionTime,
                    Sort  = item.Sort,
                    InitialState  = item.InitialState,
                    DestinationState  = item.DestinationState,
                    TriggerName  = item.TriggerName,
                    Commentary  = item.Commentary,
                    Employee = employee
                });
            }

            return histories;
        }
    }
}
