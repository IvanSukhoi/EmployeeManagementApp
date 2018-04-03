using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings;
using EmployeeManagement.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IEmployeeService _employeeService;
        private IMapperFactory<EmployeeModel> _factory;

        public AdminController(IEmployeeService employeeService, IMapperFactory<EmployeeModel> mapperFactory)
        {
            _employeeService = employeeService;
            _factory = mapperFactory;
        }

        public ViewResult Index()
        {
            var result = new List<EmployeeModel>();
            var employees = _employeeService.GetAll();
            
            foreach (var employee in employees)
            {
                var employeeModel = _factory.GetEmployeeModel<EmployeeModel>(employee);
                if (employeeModel != null)
                {
                    result.Add(employeeModel);
                }
            }

            return View(result);
        }

        public ViewResult Edit(int id)
        {
            var employee = _employeeService.Get(id);
            var employeeModel = _factory.GetEmployeeModel<EmployeeModel>(employee);

            return View(employeeModel);
        }
    }
}