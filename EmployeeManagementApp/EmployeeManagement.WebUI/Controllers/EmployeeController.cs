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
        private readonly IEmployeeModelFactory<EmployeeModel> _mapperFactory;
        public int PageSize = 4;

        public EmployeeController(IEmployeeService employeeService, IEmployeeModelFactory<EmployeeModel> mapperFactory)
        {
            _employeeService = employeeService;
            _mapperFactory = mapperFactory;
        }

        public ViewResult List(string category, int managerId, int page = 1)
        {
            List<EmployeeModel> employeeModels = null;

            if (managerId == 0)
            {
                employeeModels = _employeeService.GetAll().Select(x => _mapperFactory.MappEmployeeToEmployeeModel<EmployeeModel>(x)).ToList();
            }
            else
            {
                employeeModels = GetTreeEmployeeList(managerId);
            }

            TempData["ManagerId"] = managerId;

            EmployeeListModel model = new EmployeeListModel
            {
                ManagerId = managerId,

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

        public List<EmployeeModel> GetTreeEmployeeList(int managerId)
        {
            var managerModel = _mapperFactory.MappEmployeeToEmployeeModel<ManagerModel>(_employeeService.Get(managerId));
            var employeeModels = _employeeService.GetByManagerId(managerId)
                .Select(x => _mapperFactory.MappEmployeeToEmployeeModel<EmployeeModel>(x)).ToList();

            TempData["Manager"] = string.Format("{0}", managerModel.FirstName + managerModel.LastName);

            return employeeModels;
        }

        public ViewResult GetManagerEmployee(int employeeId)
        {
            var employeeModel = _mapperFactory.MappEmployeeToEmployeeModel<EmployeeModel>(_employeeService.Get(employeeId));

            var managerModel = _mapperFactory.MappEmployeeToEmployeeModel<ManagerModel>(_employeeService.Get((int)employeeModel.ManagerID));

            TempData["Employee"] = string.Format("{0}", employeeModel.FirstName + employeeModel.LastName);

            return View(managerModel);
        }
    }
}
