using AutoMapper;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Data.EF.Entities
{
    public interface IMapperFactory<T>
    {
        T GetEmployee<T>(EmployeeEntity employeeEntity) where T : Employee;
        EmployeeEntity GetEmployeeEF<T>(T employee) where T : Employee;
    }
   
    public class MapperFactory : IMapperFactory<Employee>
    {
        private IMapper _mapper;
        public MapperFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T GetEmployee<T>(EmployeeEntity employeeEntity) where T : Employee
        {
            Employee employee = null;
            if (employeeEntity.Profession == Profession.Developer)
            {
                employee = _mapper.Map<EmployeeEntity, Developer>(employeeEntity);
                var developer = employee as T;
                return developer;
            }

            if(employeeEntity.Profession == Profession.Manager)
            {
                employee = _mapper.Map<EmployeeEntity, Manager>(employeeEntity);
                var manager = employee as T;
                return manager;
            }

            employee = _mapper.Map<EmployeeEntity, ServiceWorker>(employeeEntity);
            var serviceWorker = employee as T;
            return serviceWorker;
        }

        public EmployeeEntity GetEmployeeEF<T>(T employee)  where T : Employee
        {
            EmployeeEntity employeeEntity = null;
            
            if (employee is Developer)
            {                     
                var developer = employee as Developer;
                employeeEntity = _mapper.Map<Developer, EmployeeEntity>(developer);
                return employeeEntity;
            }

            if (employee is Manager)
            {
                var manager = employee as Manager;
                employeeEntity = _mapper.Map<Manager, EmployeeEntity>(manager);
                return employeeEntity;
            }

            var serviceWorker = employee as ServiceWorker;
            employeeEntity = _mapper.Map<ServiceWorker, EmployeeEntity>(serviceWorker);

            return employeeEntity;
        }

    }
}
