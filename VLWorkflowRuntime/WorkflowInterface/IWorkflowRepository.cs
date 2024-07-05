using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VLWorkflowRuntime.Workflow;
using VLWorkflowRuntime.WorkflowModels;

namespace VLWorkflowRuntime.WorkflowInterface
{
    public interface IWorkflowRepository
    {
        ProcessActivity GetInitialActivityBySchemaCode(string v);
        Process GetProcessBySchemeCode(string workFlowCode);
        List<string> GetAllWorkFlows();
        ProcessCommand[] GetAvailableCommands(string workflowSchemeCode, Guid id, string v);
        ProcessActivity[] GetAvailableActivities(string workflowCode, Guid id, string user);
        Task ExecuteCommandAsync<T>(WorkflowDbContext workflowDbContext,string command, Guid id, string currentUser, string workflowcode);
    }
}
