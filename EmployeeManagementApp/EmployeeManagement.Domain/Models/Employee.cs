namespace EmployeeManagement.Domain.Models
{
    public abstract class Employee
    {       
        public int? ManagerID { get; set; }
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public Department Department { get; set; }
    }
}
