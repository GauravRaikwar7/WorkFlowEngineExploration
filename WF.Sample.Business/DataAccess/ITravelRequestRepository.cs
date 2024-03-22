using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Sample.Business.DataAccess
{
    public interface ITravelRequestRepository
    {
        Model.TravelRequest InsertOrUpdate(Model.TravelRequest doc);
        List<Model.TravelRequest> Get(out int count, int page = 1, int pageSize = 128);
        Model.TravelRequest Get(Guid id, bool loadChildEntities = true);
        Model.TravelRequest GetByNumber(int number);
        Model.TravelRequest GetByTrNumber(string number);
        List<Model.TravelRequest> GetByIds(List<Guid> ids);
        void Delete(Guid[] ids);
        void ChangeState(Guid id, string nextState, string nextStateName);
        bool IsAuthorsBoss(Guid TravelRequestId, Guid identityId);
        IEnumerable<string> GetAuthorsBoss(Guid TravelRequestId);
    }
}
