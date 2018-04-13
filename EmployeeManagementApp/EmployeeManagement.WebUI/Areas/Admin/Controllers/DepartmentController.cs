using AutoMapper;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentService _departmentService;
        IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public ViewResult Index()
        {
            var result = _departmentService.GetAll().Select(x => _mapper.Map<Department, DepartmentModel>(x)).ToList();

            return View(result);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var departmentModel = _mapper.Map<Department, DepartmentModel>(_departmentService.Get(id));

            return View(departmentModel);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentModel departmentModel)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<DepartmentModel, Department>(departmentModel);

                if (departmentModel.Id == 0)
                {
                    _departmentService.Create(department);
                }
                else
                {
                    _departmentService.Update(department);
                }

                TempData["message"] = string.Format("{0} department has been saved", department.Name);
   
                return RedirectToAction("Index");
            }
            else
            {
                return View(departmentModel);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new DepartmentModel());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var department = _departmentService.Get(id);
            _departmentService.Delete(id);
            if (department != null)
            {
                TempData["message"] = string.Format("{0} department was deleted", department.Name);
            }
            return RedirectToAction("Index");
        }
    }
}