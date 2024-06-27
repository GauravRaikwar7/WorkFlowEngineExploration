using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WF.Sample.Models
{
    public class TravelRequestModel
    {
        public static string NotFoundError = "TravelRequest is not found";
        public Guid Id { get; set; }
        public int? Number { get; set; }

        [Required]
        [StringLength(256)]
        [DataType(DataType.Text)]
        [Display(Name = "WorkFlowSchemeCode")]
        public string WorkflowSchemeCode { get; set; }
        
        [Required]
        [StringLength(256)]
        [DataType(DataType.Text)]
        [Display(Name = "Travel Request Number")]
        public string TravelRequestNumber { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }
        
        public Guid AuthorId { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Originator")]
        public string AuthorName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Manager")]
        public Guid? ManagerId { get; set; }
        
        
        [DataType(DataType.Text)]
        [Display(Name = "Manager")]
        public string ManagerName { get; set; }

        [Display(Name = "TotalCost")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Status")]
        public string StateName { get; set; }
        
        //False - if TravelRequest is not found
        public bool IsCorrect { get; set; } = true;
        

        public TravelRequestCommandModel[] Commands { get; set; }

        public Dictionary<string, string> AvailiableStates { get; set; }

        public TravelRequestModel ()
        {
            Commands = new TravelRequestCommandModel[0];
            AvailiableStates = new Dictionary<string, string>{};
            HistoryModel = new TravelRequestHistoryModel();
        }

        public TravelRequestModel(string stateName)
        {
            StateName = stateName ?? string.Empty;
            Commands = new TravelRequestCommandModel[0];
            AvailiableStates = new Dictionary<string, string> { };
            HistoryModel = new TravelRequestHistoryModel();
        }

        public string StateNameToSet { get; set; }

        public TravelRequestHistoryModel HistoryModel { get; set; }
    }

    public class TravelRequestCommandModel
    {
        public string key { get; set; }
        public string value { get; set; }
        public OptimaJet.Workflow.Core.Model.TransitionClassifier Classifier { get; set; }
    }
}
