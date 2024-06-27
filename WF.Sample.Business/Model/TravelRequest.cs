using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Sample.Business.Model
{
    public class TravelRequest
    {
        public Guid Id { get; set; }
        public string TravelRequestNumber { get; set; }
        public string WorkflowSchemeCode { get; set; }
        public int? Number { get; set; }
        public string Comment { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? ManagerId { get; set; }
        public decimal TotalCost { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public Employee Author { get; set;}
        public Employee Manager { get; set; }
    }
}
