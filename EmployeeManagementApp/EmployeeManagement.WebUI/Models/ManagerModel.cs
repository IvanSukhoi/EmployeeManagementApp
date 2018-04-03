using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    public class ManagerModel : EmployeeModel
    {
        public List<int> Employees { get; set; }
    }
}