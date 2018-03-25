using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    public class DepartmentModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<int> CatalogEmployeeID { get; set; }
    }
}