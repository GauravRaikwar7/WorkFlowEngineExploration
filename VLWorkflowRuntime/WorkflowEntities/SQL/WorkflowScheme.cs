namespace VLWorkflowRuntime.WorkflowEntities.SQL
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("WorkflowProcessScheme")]
    public partial class WorkflowProcessScheme
    {
        public Guid Id { get; set; }

        public string Scheme { get; set; }

        public string DefiningParameters { get; set; }

        public string DefiningParametersHash { get; set; }

        public string SchemeCode { get; set; }

        public bool IsObsolete { get; set; }

        public string RootSchemeCode { get; set; }

        public Guid? RootSchemeId { get; set; }

        public string AllowedActivities { get; set; }

        public string StartingTransition { get; set; }

    }
}
