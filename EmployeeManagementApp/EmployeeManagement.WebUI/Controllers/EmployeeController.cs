using AutoMapper;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public ViewResult List()
        {
            var model = _employeeService.GetAll();
            var list = Mapper.Map<List<Employee>, List<EmployeeViewModel>>(model);
            return View(list);
        }
    }
}
