using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Xml.Linq;
using OptimaJet.Workflow.Core.Runtime;
using WF.Sample.Business.DataAccess;
using WF.Sample.Business.Model;

namespace WF.Sample.Business.Workflow
{
    public class AutoCompleteProvider: IDesignerAutocompleteProvider
    {
        public ISettingsProvider repository { get; set; }
        public AutoCompleteProvider(ISettingsProvider employeeRepository) {
            repository = employeeRepository;
        }

        public List<string> GetAutocompleteSuggestions(SuggestionCategory category, string value, string schemeCode)
        {
            if (category == SuggestionCategory.RuleParameter && value == "CheckRole")
            {
                var roles = repository.GetAllRoles();
                return roles.Select(x=> x.Name).ToList();
            }

            if (category == SuggestionCategory.ActionParameter && value == "SendEmailToManager")
            {

                return new List<string>();
            }

            if (category == SuggestionCategory.ConditionParameter && value == "IsNotifyToManager")
            {
                PropertyInfo[] properties = typeof(TravelRequest).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                List<string> publicProperties = new List<string>();
                foreach (PropertyInfo property in properties)
                {
                    publicProperties.Add("@"+property.Name);
                }

                return publicProperties;
            }

            return null;
        }
    }
}
