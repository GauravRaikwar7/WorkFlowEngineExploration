using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WF.Sample.Business.Model;

namespace WF.Sample.Business.DataAccess
{
    public interface IWorkflowRepository
    {
        ProcessActivity GetInitialActivityBySchemaCode(string v);
        Process GetProcessBySchemeCode(string workFlowCode);
        List<string> GetAllWorkFlows();
        ProcessCommand[] GetAvailableCommands(string workflowSchemeCode, Guid id, string v);
        ProcessActivity[] GetAvailableActivities(string workflowCode, Guid id, string user);
        Task ExecuteCommandAsync<T>(string command, Guid id, string currentUser, string workflowcode);
    }
}
