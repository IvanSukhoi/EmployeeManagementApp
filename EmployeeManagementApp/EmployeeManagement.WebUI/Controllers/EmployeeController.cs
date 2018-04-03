using AutoMapper;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.WebUI.Mappings;
using EmployeeManagement.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapperFactory<EmployeeModel> _mapperFactory;
        public int PageSize = 4;

        public EmployeeController(IEmployeeService employeeService, IMapperFactory<EmployeeModel> mapperFactory)
        {
            _employeeService = employeeService;
            _mapperFactory = mapperFactory;
        }

        public ViewResult List(string category, int page = 1)
        {
            var employees = _employeeService.GetAll();
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();

            foreach (var employee in employees)
            {
                var employeeModel = _mapperFactory.GetEmployeeModel<EmployeeModel>(employee);
                employeeModels.Add(employeeModel);
            }

            EmployeeListModel model = new EmployeeListModel
            {
                Employees = employeeModels
                .Where(x => category == null || x.DepartmentName == category)
                .OrderBy(x => x.FirstName)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? employeeModels.Count() :
                        employeeModels.Where(e => e.DepartmentName == category).Count()
                },

                CurrentCategory = category
            };
            
            return View(model);
        }
    }
}
