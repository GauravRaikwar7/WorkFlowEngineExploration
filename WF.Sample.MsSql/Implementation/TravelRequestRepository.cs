using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Sample.Business.DataAccess;
using WF.Sample.Business.Workflow;


namespace WF.Sample.MsSql.Implementation
{
    public class TravelRequestRepository : ITravelRequestRepository
    {
        private readonly SampleContext _sampleContext;

        public TravelRequestRepository(SampleContext sampleContext)
        {
            _sampleContext = sampleContext;
        }

        public void ChangeState(Guid id, string nextState,  string nextStateName)
        {
            var TravelRequest = GetTravelRequest(id);
            if (TravelRequest == null)
                return;

            TravelRequest.State = nextState;
            TravelRequest.StateName = nextStateName;
            
            _sampleContext.SaveChanges();
        }
        
        public void Delete(Guid[] ids)
        {
            var objs = _sampleContext.TravelRequests.Where(x => ids.Contains(x.Id));
            
            foreach (var id in ids)
            {
                WorkflowInit.Runtime.PersistenceProvider.DropWorkflowInboxAsync(id).GetAwaiter().GetResult();
                WorkflowInit.Runtime.PersistenceProvider.DropApprovalHistoryByProcessIdAsync(id).GetAwaiter().GetResult();
            }
            
            _sampleContext.TravelRequests.RemoveRange(objs);

            _sampleContext.SaveChanges();
        }

        public List<Business.Model.TravelRequest> Get(out int count, int page = 1, int pageSize = 128)
        {
            page -= 1;
            int actual = page * pageSize;
            var query = _sampleContext.TravelRequests.OrderByDescending(c => c.Number);
            count = query.Count();
            return query.Include(x => x.Author)
                        .Include(x => x.Manager)
                        .Skip(actual)
                        .Take(pageSize)
                        .ToList()
                        .Select(d => Mappings.Mapper.Map<Business.Model.TravelRequest>(d)).ToList();
        }
        
        public List<Business.Model.TravelRequest> GetByIds(List<Guid> ids)
        {
            var query = _sampleContext.TravelRequests.Where(x => ids.Contains(x.Id));
            return query.Include(x => x.Author)
                .Include(x => x.Manager)
                .ToList()
                .Select(d => Mappings.Mapper.Map<Business.Model.TravelRequest>(d)).ToList();
        }

        public IEnumerable<string> GetAuthorsBoss(Guid TravelRequestId)
        {
            var TravelRequest = _sampleContext.TravelRequests.Find(TravelRequestId);
            if (TravelRequest == null)
                return new List<string> { };

            return
                _sampleContext.VHeads.Where(h => h.Id == TravelRequest.AuthorId)
                    .Select(h => h.HeadId)
                    .ToList()
                    .Select(c => c.ToString());
        }

        public Business.Model.TravelRequest InsertOrUpdate(Business.Model.TravelRequest doc)
        {
            TravelRequest target = null;
            if (doc.Id != Guid.Empty)
            {
                target = _sampleContext.TravelRequests.Find(doc.Id);
                if (target == null)
                {
                    return null;
                }
            }
            else
            {
                target = new TravelRequest
                {
                    Id = Guid.NewGuid(),
                    AuthorId = doc.AuthorId,
                    StateName = doc.StateName
                };
                _sampleContext.TravelRequests.Add(target);
            }

            target.TravelRequestNumber = doc.TravelRequestNumber;
            target.ManagerId = doc.ManagerId;
            target.Comment = doc.Comment;
            target.TotalCost = doc.TotalCost;

            _sampleContext.SaveChanges();

            doc.Id = target.Id;
            doc.Number = target.Number;

            return doc;
        }

        public bool IsAuthorsBoss(Guid TravelRequestId, Guid identityId)
        {
            var TravelRequest = _sampleContext.TravelRequests.Find(TravelRequestId);
            if (TravelRequest == null)
                return false;
            return _sampleContext.VHeads.Count(h => h.Id == TravelRequest.AuthorId && h.HeadId == identityId) > 0;
        }

        public Business.Model.TravelRequest Get(Guid id, bool loadChildEntities = true)
        {
            TravelRequest TravelRequest = GetTravelRequest(id, loadChildEntities);
            if (TravelRequest == null) return null;
            return Mappings.Mapper.Map<Business.Model.TravelRequest>(TravelRequest);
        }
        
        public Business.Model.TravelRequest GetByNumber(int number)
        {
            var TravelRequest = _sampleContext.TravelRequests.FirstOrDefault(d => d.Number == number);
            if (TravelRequest == null) return null;
            return Mappings.Mapper.Map<Business.Model.TravelRequest>(TravelRequest);
        }

        public Business.Model.TravelRequest GetByTrNumber(string number)
        {
            var TravelRequest = _sampleContext.TravelRequests.FirstOrDefault(d => d.TravelRequestNumber == number);
            if (TravelRequest == null) return null;
            return Mappings.Mapper.Map<Business.Model.TravelRequest>(TravelRequest);
        }

        private TravelRequest GetTravelRequest(Guid id, bool loadChildEntities = true)
        {
            TravelRequest TravelRequest = null;

            if (!loadChildEntities)
            {
                TravelRequest = _sampleContext.TravelRequests.Find(id);
            }
            else
            {
                TravelRequest = _sampleContext.TravelRequests
                                         .Include(x => x.Author)
                                         .Include(x => x.Manager).FirstOrDefault(x => x.Id == id);
            }

            return TravelRequest;

        }

        private string GetEmployeesString(IEnumerable<string> identities)
        {
            var identitiesGuid = identities.Select(c => new Guid(c));

            var employees = _sampleContext.Employees.Where(e => identitiesGuid.Contains(e.Id)).ToList();

            var sb = new StringBuilder();
            bool isFirst = true;
            foreach (var employee in employees)
            {
                if (!isFirst)
                    sb.Append(",");
                isFirst = false;

                sb.Append(employee.Name);
            }

            return sb.ToString();
        }
    }
}
