using AutoMapper;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;

namespace EmployeeManagement.WebUI.Mappings
{
    public interface IMapperFactory<T>
    {
        T GetEmployeeModel<T>(Employee employee) where T : EmployeeModel;
        Employee GetEmployee<T>(T employee) where T : EmployeeModel;
    }

    public class MapperFactory : IMapperFactory<EmployeeModel>
    {
        private IMapper _mapper;
        public MapperFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T GetEmployeeModel<T>(Employee employee) where T : EmployeeModel
        {
            EmployeeModel employeeModel = null;
            if (employee as Developer != null)
            {
                var developer = employee as Developer;
                employeeModel = _mapper.Map<Developer, DeveloperModel>(developer);

                return employeeModel as T;
            }

            if (employee as Manager != null)
            {
                var manager = employee as Manager;
                employeeModel = _mapper.Map<Manager, ManagerModel>(manager);

                return employeeModel as T;
            }

            var serviceWorker = employee as ServiceWorker;
            employeeModel = _mapper.Map<ServiceWorker, ServiceWorkerModel>(serviceWorker);

            return employeeModel as T;
        }

        public Employee GetEmployee<T>(T employeeModel) where T : EmployeeModel
        {
            Employee employee = null;
            if (employeeModel as DeveloperModel != null)
            {
                var developerModel = employeeModel as DeveloperModel;
                employee = _mapper.Map<DeveloperModel, Developer>(developerModel);

                return employee;
            }

            if (employeeModel as Manager != null)
            {
                var managerModel = employeeModel as ManagerModel;
                employee = _mapper.Map<ManagerModel, Manager>(managerModel);

                return employee;
            }

            var serviceWorkerModel = employeeModel as ServiceWorkerModel;
            employee = _mapper.Map<ServiceWorkerModel, ServiceWorker>(serviceWorkerModel);

            return employee;
        }
    }
}