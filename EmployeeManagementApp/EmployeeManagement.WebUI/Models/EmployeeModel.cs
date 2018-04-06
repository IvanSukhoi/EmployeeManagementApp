using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.Models
{

    public abstract class EmployeeModel
    {
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }

        public int ID { get; set; }
        public int? ManagerID { get; set; }

        [ScaffoldColumn(false)]
        public int DepartmentID { get; set; }

        public Sex Sex { get; set; }

        public string DepartmentName { get; set; }

        public ActionMethod ActionMethod { get; set; }
        public ModelType ModelType { get; set; }

        public List<DepartmentModel> DepartmentModelList { get; set; }
    }
}