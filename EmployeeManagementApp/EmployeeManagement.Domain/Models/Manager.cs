using System.Collections.Generic;

namespace EmployeeManagement.Domain.Models
{
    public class Manager : Employee
    {
        public List<Employee> Employees { get; set; }
    }
}
