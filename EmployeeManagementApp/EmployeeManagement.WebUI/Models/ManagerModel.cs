using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    public class ManagerModel : EmployeeModel
    {
        public List<int> Employees { get; set; }
        public List<string> EmployeeNames { get; set; }

        public ManagerModel()
        {
            EmployeeNames = new List<string>();
        }
    }
}