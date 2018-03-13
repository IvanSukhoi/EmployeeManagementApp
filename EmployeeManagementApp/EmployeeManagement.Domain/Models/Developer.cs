using System.Collections.Generic;

namespace EmployeeManagement.Domain.Models
{
    public class Developer : Employee
    {
        public Position? Position { get; set; }
        public List<string> Tasks { get; set; }
    }
}
