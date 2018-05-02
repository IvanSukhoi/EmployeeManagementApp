using Fluent_Validtion.Models.Validators;
using System.Collections.Generic;

namespace EmployeeManagement.WebUI.Models
{
    [FluentValidation.Attributes.Validator(typeof(RegisterModelValidator))]
    public abstract class EmployeeModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int ID { get; set; }
        public int? ManagerID { get; set; }
        public int DepartmentID { get; set; }

        public Sex Sex { get; set; }

        public string DepartmentName { get; set; }

        public ActionMethod ActionMethod { get; set; }
        public ModelType ModelType { get; set; }

        public List<DepartmentModel> DepartmentModelList { get; set; }
    }
}