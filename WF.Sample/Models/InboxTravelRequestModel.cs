using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OptimaJet.Workflow.Core.Persistence;

namespace WF.Sample.Models
{
    public class InboxTravelRequestModel:TravelRequestModel
    {
        [Display(Name = "AddingDate")]
        public string AddingDate { get; set; }
        [Display(Name = "AvailableCommands")]
        public List<CommandName> AvailableCommands { get; set; }

    }
}
