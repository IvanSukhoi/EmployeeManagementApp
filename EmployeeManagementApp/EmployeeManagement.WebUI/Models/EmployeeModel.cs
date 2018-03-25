namespace EmployeeManagement.WebUI.Models
{
    public abstract class EmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ID { get; set; }
        public int? ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public string MidleName { get; set; }
        public Sex Sex { get; set; }
        public string DepartmentName { get; set; }
    }
}