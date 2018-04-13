using AutoMapper;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings;
using EmployeeManagement.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private IDepartmentService _departmentService;
        private IEmployeeModelFactory<EmployeeModel> _mapperFactory;
        private IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IMapper mapper, IEmployeeModelFactory<EmployeeModel> mapperFactory)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapperFactory = mapperFactory;
            _mapper = mapper;
        }

        public ViewResult Index()
        {
            var result = new List<EmployeeModel>();
            var employees = _employeeService.GetAll();

            return View(employees.Select(x => _mapperFactory.MappEmployeeToEmployeeModel<EmployeeModel>(x)).ToList());
        }

        [HttpGet]
        public ViewResult EditManager(int id)
        {
            var managerModel = _mapperFactory.MappEmployeeToEmployeeModel<ManagerModel>(_employeeService.Get(id));
            BuildEmployeeModel(managerModel);

            return View(managerModel);
        }

        [HttpGet]
        public ViewResult EditDeveloper(int id)
        {
            var developerModel = _mapperFactory.MappEmployeeToEmployeeModel<DeveloperModel>(_employeeService.Get(id));
            BuildEmployeeModel(developerModel);

            return View(developerModel);
        }

        [HttpGet]
        public ViewResult EditServiceWorker(int id)
        {
            var serviceWorkerModel = _mapperFactory.MappEmployeeToEmployeeModel<ServiceWorkerModel>(_employeeService.Get(id));
            BuildEmployeeModel(serviceWorkerModel);

            return View(serviceWorkerModel);
        }

        [HttpPost]
        public ActionResult EditDeveloper(DeveloperModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                SaveEmployeeModel(employeeModel);
               
                return RedirectToAction("Index");
            }
            else
            {
                return View(employeeModel);
            }
        }

        [HttpPost]
        public ActionResult EditManager(ManagerModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                SaveEmployeeModel(employeeModel);

                return RedirectToAction("Index");
            }
            else
            {
                return View(employeeModel);
            }
        }

        [HttpPost]
        public ActionResult EditServiceWorker(ServiceWorkerModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                SaveEmployeeModel(employeeModel);

                return RedirectToAction("Index");
            }
            else
            {
                return View(employeeModel);
            }
        }

        public ViewResult CreateDeveloper()
        {
            var departmentModels = _departmentService.GetAll().Select(x => _mapper.Map<Department, DepartmentModel>(x)).ToList();

            return View("EditDeveloper", new DeveloperModel { DepartmentModelList = departmentModels, ActionMethod = ActionMethod.Create});
        }

        public ViewResult CreateManager()
        {
            var departmentModels = _departmentService.GetAll().Select(x => _mapper.Map<Department, DepartmentModel>(x)).ToList();

            return View("EditManager", new ManagerModel { DepartmentModelList = departmentModels, ActionMethod = ActionMethod.Create});
        }

        public ViewResult CreateServiceWorker()
        {
            var departmentModels = _departmentService.GetAll().Select(x => _mapper.Map<Department, DepartmentModel>(x)).ToList();

            return View("EditServiceWorker", new ServiceWorkerModel { DepartmentModelList = departmentModels, ActionMethod = ActionMethod.Create});
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = _employeeService.Get(id);
            _employeeService.Delete(id);
            if (employee != null)
            {
                TempData["message"] = string.Format("{0} was deleted", employee.FirstName + employee.LastName);
            }
            return RedirectToAction("Index");
        }

        private void BuildEmployeeModel(EmployeeModel employeeModel)
        {
            employeeModel.ActionMethod = ActionMethod.Edit;
            employeeModel.DepartmentModelList = _departmentService.GetAll().Select(x => _mapper.Map<Department, DepartmentModel>(x)).ToList();
        }

        private void SaveEmployeeModel(EmployeeModel employeeModel)
        {
            var employee = _mapperFactory.MappEmployeeModelToEmployee(employeeModel);
            TempData["message"] = string.Format("{0} has been saved", employee.FirstName + employee.LastName);

            if (employee.ID == 0)
            {
                _employeeService.Create(employee);
            }
            else
            {
                _employeeService.Update(employee);
            }
        }
    }
}