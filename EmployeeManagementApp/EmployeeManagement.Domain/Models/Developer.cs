using System.Collections.Generic;

namespace EmployeeManagement.Domain.Models
{
    public class Developer : Employee
    {
        public Position Position { get; set; }
        public List<Task> Tasks { get; set; }
    }

    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
    }
}
