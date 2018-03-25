using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    public class DeveloperModel : EmployeeModel
    {
        public Position Position { get; set; }
        public List<Task> Tasks { get; set; }
    }
}