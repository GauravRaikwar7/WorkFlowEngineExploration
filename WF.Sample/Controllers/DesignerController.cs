using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OptimaJet.Workflow;
using WF.Sample.Business.DataAccess;
using WF.Sample.Business.Workflow;
using WF.Sample.Extensions;
using WF.Sample.Helpers;


namespace WF.Sample.Controllers
{
    public class DesignerController : Controller
    {
        public IEmployeeRepository _empRepository { get; set; }

        public DesignerController(IEmployeeRepository employeeRepository) {
        _empRepository = employeeRepository;
        }
        public Task<ActionResult> Index(string schemeName)
        {
            var identityId = CurrentUserSettings.GetCurrentUser(HttpContext);
            if(_empRepository.GetById(identityId).EmployeeRoles != null &&
                _empRepository.GetById(identityId).EmployeeRoles.Any(x=> x.Role.Name == "Big Boss"))
            {
                return Task.FromResult<ActionResult>(View());
            }
            string customHtml = "<!DOCTYPE html><html><head><title>User does not have permissions</title></head><body><h1>User does not have sufficient permissions, to go back to home page, click here - </h1></body></html>" +
                "<a href=\"https://localhost:44338/\"><h1>HOME</h1></a>";
            return Task.FromResult<ActionResult>(Content(customHtml, "text/html"));
        }
        
        public async Task<ActionResult> API()
        {
            Stream filestream = null;
            var isPost = Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase);
            if (isPost && Request.Form.Files != null && Request.Form.Files.Count > 0)
                filestream = Request.Form.Files[0].OpenReadStream();

            var pars = new NameValueCollection();
            foreach (var q in Request.Query)
            {
                pars.Add(q.Key, q.Value.First());
            }


            if (isPost)
            {
                var parsKeys = pars.AllKeys;
                //foreach (var key in Request.Form.AllKeys)
                foreach (string key in Request.Form.Keys)
                {
                    if (!parsKeys.Contains(key))
                    {
                        pars.Add(key, Request.Form[key]);
                    }
                }
            }

            (string res, bool hasError) = await WorkflowInit.Runtime.DesignerAPIAsync(pars, filestream);
            
            var operation = pars["operation"].ToLower();
            if (operation == "downloadscheme" && !hasError)
                return File(Encoding.UTF8.GetBytes(res), "text/xml");
            else if (operation == "downloadschemebpmn" && !hasError)
                return File(UTF8Encoding.UTF8.GetBytes(res), "text/xml");

            return Content(res);
        }

     }
}
