using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    public class DeveloperModel : EmployeeModel
    {
        public List<Task> Tasks { get; set; }
        public Position Position { get; set; }
    }
}