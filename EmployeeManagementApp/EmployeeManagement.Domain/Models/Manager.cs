using System.Collections.Generic;

namespace EmployeeManagement.Domain.Models
{
    public class Manager : Employee
    {
        public List<int> EmployeeID { get; set; }
    }
}
