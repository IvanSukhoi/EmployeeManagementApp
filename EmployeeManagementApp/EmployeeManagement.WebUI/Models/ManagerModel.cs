using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    public class ManagerModel : EmployeeModel
    {
        public List<EmployeeModel> Employees { get; set; }
    }
}