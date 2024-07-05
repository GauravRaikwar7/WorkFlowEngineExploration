using Microsoft.EntityFrameworkCore;
using VLWorkflowRuntime.Workflow;
using VLWorkflowRuntime.WorkflowInterface;
using VLWorkflowRuntime.WorkflowModels;


namespace VLWorkflowRuntime.WorkflowImplementation
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly Workflow.WorkflowDbContext _sampleContext;

        public WorkflowRepository(WorkflowDbContext workflowDbContext)
        {
            _sampleContext = workflowDbContext;
        }

        public ProcessActivity GetInitialActivityBySchemaCode(string workFlowCode)
        {
            var process = GetProcessBySchemeCode(workFlowCode);
            if (process != null)
            {
                return process.Activities.FirstOrDefault(x => x.IsInitial);
            }
            return null;
        }

        public List<string> GetAllWorkFlows()
        {
            return _sampleContext.WorkflowSchemes.Select(x => x.Code).ToList();
        }

        public Process GetProcessBySchemeCode(string workFlowCode)
        {
            Process process = null;
            var scheme = _sampleContext.WorkflowSchemes.FirstOrDefault(x => x.Code == workFlowCode);
            if (scheme != null && !string.IsNullOrEmpty(scheme.Scheme))
            {
                process = XmlDeserializer.Deserialize<Process>(scheme.Scheme);
            }
            return process;
        }

        public ProcessCommand[] GetAvailableCommands(string workFlowCode, Guid id, string v)
        {
            var commands = GetProcessBySchemeCode(workFlowCode).Commands;
            return commands;
        }

        public ProcessActivity[] GetAvailableActivities(string workflowCode, Guid id, string user)
        {
            var activities = GetProcessBySchemeCode(workflowCode).Activities;
            return activities;
        }

        public async Task ExecuteCommandAsync<T>(WorkflowDbContext _sampleContext, string command, Guid id, string currentUser, string workflowcode)
        {
            var scheme = GetProcessBySchemeCode(workflowcode);
            var wfpi = await _sampleContext.WorkflowProcessInstance.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

            var currentState = wfpi.StateName;
            ProcessTransition transition = GetTransition(command, currentState, scheme);
            string newState = transition.To;
            string newStateName = GetStateName(scheme, newState);

            wfpi.PreviousState = wfpi.StateName;
            wfpi.PreviousActivity = wfpi.ActivityName;
            wfpi.StateName = newStateName;
            wfpi.ActivityName = newState;

            var entityTypes = _sampleContext.Model.GetEntityTypes();
            var entityType = entityTypes.FirstOrDefault(et => et.ClrType == typeof(T));
            var tableName = entityType.GetTableName();

            var query = $"update dbo.{tableName} set state = '{newState}', statename = '{newStateName}' where id = '{id}' ";
            var res = await _sampleContext.Database.ExecuteSqlRawAsync(query);
            if (res != 1)
            {
                //throw new Exception("Some issue with saving new state in entity table");
            }
            else
            {
                await _sampleContext.SaveChangesAsync();
                await ExecuteAutoTransition(wfpi, newState, scheme, tableName);
            }

            return;
        }

        private async Task ExecuteAutoTransition(WorkflowEntities.SQL.WorkflowProcessInstance wfpi, string currentState, Process scheme, string tableName)
        {
            ProcessTransition processTransition = GetAutoTransition(currentState, scheme);
            if (processTransition != null)
            {
                string newState = processTransition.To;
                string newStateName = GetStateName(scheme, newState);
                var query = $"update dbo.{tableName} set state = '{newState}', statename = '{newStateName}' where id = '{wfpi.Id}' ";
                var res = await _sampleContext.Database.ExecuteSqlRawAsync(query);
                if (res == 1)
                {
                    wfpi.PreviousState = wfpi.StateName;
                    wfpi.PreviousActivity = wfpi.ActivityName;
                    wfpi.StateName = newStateName;
                    wfpi.ActivityName = newState;
                    await _sampleContext.SaveChangesAsync();
                }
            }
        }

        private ProcessTransition GetAutoTransition(string currentState, Process scheme)
        {
            var transitions = scheme.Transitions.Where(x => x.From == currentState && x.Triggers.Trigger.Type.ToLower() == "auto").ToList();
            return transitions.FirstOrDefault();
        }

        private string GetStateName(Process scheme, string newState)
        {
            return scheme.Activities.FirstOrDefault(x => x.State == newState)?.Name;
        }

        private ProcessTransition GetTransition(string command, string currentState, Process scheme)
        {
            var transitions = scheme.Transitions.Where(x => x.From == currentState && x.Triggers.Trigger.NameRef == command).ToList();
            transitions = GetFilteredTransitionByRestriction(transitions, command, currentState);
            transitions = GetFilteredTransitionByCondition(transitions, command, currentState);
            return transitions.FirstOrDefault();
        }

        private List<ProcessTransition> GetFilteredTransitionByCondition(List<ProcessTransition> transitions, string command, string currentState)
        {
            return transitions;
        }

        private List<ProcessTransition> GetFilteredTransitionByRestriction(List<ProcessTransition> transitions, string command, string currentState)
        {
            return transitions;
        }
    }
}
