using EmployeeManagement.Domain.DomainInterfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IDepartmentService _departmnetService;
        // GET: Nav

        public NavController(IDepartmentService departmentService)
        {
            _departmnetService = departmentService;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            var departments = _departmnetService.GetAll();
            IEnumerable<string> categories = departments
                .Select(x => x.Name)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}