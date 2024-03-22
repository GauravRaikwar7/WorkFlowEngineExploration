using System.Collections.Generic;
using WF.Sample.Business.Model;

namespace WF.Sample.Models
{
    public class TravelRequestListModel<TDoc>:IPaging
        where TDoc:TravelRequestModel
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<TDoc> Docs { get; set; }
    }
    public class TravelRequestIndexModel<T> : IPaging where T : TravelRequestModel
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public TravelRequestListModel<T> TravelRequestListModel { get; set; }
        public string UserRole { get; set; }
    }

}
