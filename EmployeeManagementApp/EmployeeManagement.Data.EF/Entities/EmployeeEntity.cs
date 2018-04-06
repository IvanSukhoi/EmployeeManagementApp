using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Data.EF.Entities
{
    public class EmployeeEntity
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }

        public int? ManagerID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [DefaultValue(null)]
        public string MidleName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public Profession Profession { get; set; }
        public Position? Position { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public virtual DepartmentEntity Department { get; set; }
    }
}
