using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Data.EF.Entities
{
    public class DepartmentEntity
    {
        [Key, Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual List<int> CatalogEmployeeID { get; set; }
    }
}
