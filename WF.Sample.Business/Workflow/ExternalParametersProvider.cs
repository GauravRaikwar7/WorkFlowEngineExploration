using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OptimaJet.Workflow.Core.Model;
using OptimaJet.Workflow.Core.Runtime;
using WF.Sample.Business.Model;

namespace WF.Sample.Business.Workflow
{
    public class ExternalParametersProvider : IWorkflowExternalParametersProvider
    {
        
        public ExternalParametersProvider()
        {
            
            GetParameter.Add(nameof(TravelRequest.Author), (name, pr)=> GetTravelRequest(pr).Author);
            GetParameter.Add(nameof(TravelRequest.Manager), (name, pr)=> GetTravelRequest(pr).Manager);
            GetParameter.Add(nameof(TravelRequest.TravelRequestNumber), (name, pr)=> GetTravelRequest(pr).TravelRequestNumber);
            GetParameter.Add(nameof(TravelRequest.Number), (name, pr)=> GetTravelRequest(pr).Number);
            GetParameter.Add(nameof(TravelRequest.State), (name, pr)=> GetTravelRequest(pr).State);
            GetParameter.Add(nameof(TravelRequest.TotalCost), (name, pr)=> GetTravelRequest(pr).TotalCost);
            GetParameter.Add(nameof(TravelRequest.AuthorId), (name, pr)=> GetTravelRequest(pr).AuthorId);
            GetParameter.Add(nameof(TravelRequest.ManagerId), (name, pr)=> GetTravelRequest(pr).ManagerId);
            GetParameter.Add(nameof(TravelRequest.StateName), (name, pr)=> GetTravelRequest(pr).StateName);
            GetParameter.Add(nameof(TravelRequest), (name, pr)=> GetTravelRequest(pr));
        }
        
        
        public Dictionary<string, Func<string, ProcessInstance, object>> GetParameter = new Dictionary<string, Func<string, ProcessInstance, object>>();

        public Dictionary<string, Func<string, ProcessInstance, Task<object>>> GetParametersAsync = new Dictionary<string, Func<string, ProcessInstance, Task<object>>>();
        
        public Dictionary<string, Action<string, object, ProcessInstance>> SetParameter = new Dictionary<string, Action<string, object, ProcessInstance>>();
        
        public Dictionary<string, Func<string, object, ProcessInstance, Task>> SetParametersAsync = new Dictionary<string, Func<string, object, ProcessInstance, Task>>();

        public Func<ProcessInstance, TravelRequest> GetTravelRequest;
        public Task<object> GetExternalParameterAsync(string parameterName, ProcessInstance processInstance)
        {
            return GetParametersAsync[parameterName](parameterName, processInstance);
        }

        public object GetExternalParameter(string parameterName, ProcessInstance processInstance)
        {
            return GetParameter[parameterName](parameterName, processInstance);
        }

        public Task SetExternalParameterAsync(string parameterName, object parameterValue, ProcessInstance processInstance)
        {
            return SetParametersAsync[parameterName](parameterName, parameterValue, processInstance);
        }

        public void SetExternalParameter(string parameterName, object parameterValue, ProcessInstance processInstance)
        {
            SetParameter[parameterName](parameterName, parameterValue, processInstance);
        }

        public bool IsGetExternalParameterAsync(string parameterName, string schemeCode, ProcessInstance processInstance)
        {
            return GetParametersAsync.ContainsKey(parameterName);
        }

        public bool IsSetExternalParameterAsync(string parameterName, string schemeCode, ProcessInstance processInstance)
        {
            return SetParametersAsync.ContainsKey(parameterName);
        }

        public bool HasExternalParameter(string parameterName, string schemeCode, ProcessInstance processInstance)
        {
            return GetParameter.ContainsKey(parameterName) || SetParameter.ContainsKey(parameterName) 
                                                           || GetParametersAsync.ContainsKey(parameterName) 
                                                           || SetParametersAsync.ContainsKey(parameterName);
        }
    }
}
