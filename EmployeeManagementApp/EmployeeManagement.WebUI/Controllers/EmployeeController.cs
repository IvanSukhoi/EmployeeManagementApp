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
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public ViewResult List()
        {
            var model = _employeeService.GetAll();
            var list = _mapper.Map<List<Employee>, List<EmployeeViewModel>>(model);
            return View(list);
        }
    }
}
