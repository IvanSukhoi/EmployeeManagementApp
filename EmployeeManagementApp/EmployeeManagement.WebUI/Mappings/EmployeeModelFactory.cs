using AutoMapper;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;
using System;

namespace EmployeeManagement.WebUI.Mappings
{
    public interface IEmployeeModelFactory<T>
    {
        T MappEmployeeToEmployeeModel<T>(Employee employee) where T : EmployeeModel;
        Employee MappEmployeeModelToEmployee<T>(T employee) where T : EmployeeModel;
    }

    public class EmployeeModelFactory : IEmployeeModelFactory<EmployeeModel>
    {
        private IMapper _mapper;
        public EmployeeModelFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T MappEmployeeToEmployeeModel<T>(Employee employee) where T : EmployeeModel
        {
            
            if (employee.GetType() == typeof(Developer))
            {
                var developerModel = _mapper.Map<Developer, DeveloperModel>(employee as Developer);

                return developerModel as T;
            }

            if (employee.GetType() == typeof(Manager))
            {
                var managerModel = _mapper.Map<Manager, ManagerModel>(employee as Manager);

                return managerModel as T;
            }

            if (employee.GetType() == typeof(ServiceWorker))
            {
                var serviceWorkerModel = _mapper.Map<ServiceWorker, ServiceWorkerModel>(employee as ServiceWorker);

                return serviceWorkerModel as T;
            }

            throw new Exception("This type is can not be mapped");
        }

        public Employee MappEmployeeModelToEmployee<T>(T employeeModel) where T : EmployeeModel
        {
            if (employeeModel.GetType() == typeof(DeveloperModel))
            {
               return _mapper.Map<DeveloperModel, Developer>(employeeModel as DeveloperModel);
            }

            if (employeeModel.GetType() ==  typeof(ManagerModel))
            {
               return _mapper.Map<ManagerModel, Manager>(employeeModel as ManagerModel);
            }

            if (employeeModel.GetType() == typeof(ServiceWorkerModel))
            {
                return _mapper.Map<ServiceWorkerModel, ServiceWorker>(employeeModel as ServiceWorkerModel);
            }


            throw new Exception("This type is can not be mapped");
        }
    }
}