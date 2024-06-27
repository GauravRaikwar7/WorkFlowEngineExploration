using System.Linq;
using WF.Sample.Business.Model;

namespace WF.Sample.Extensions
{
    public static class EmployeeExtensions
    {
        public static string GetListRoles(this Employee item)
        {
            if(item == null)
            {
                return string.Empty;
            }
            return string.Join(",", item.EmployeeRoles.Select(c => c.Role.Name).ToArray());
        }
    }
}