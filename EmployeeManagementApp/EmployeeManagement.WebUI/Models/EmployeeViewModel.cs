﻿namespace EmployeeManagement.WebUI.Models
{
    public class EmployeeViewModel
    {
        public int? ManagerID { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public string DepartmentName { get; set; }
    }
}