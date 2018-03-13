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
        public T GetEmployee<T>(EmployeeEntity employeeEntity) where T : Employee
        {
            Employee employee = null;
            if (employeeEntity.Profession == Profession.Developer)
            {
                employee = Mapper.Map<EmployeeEntity, Developer>(employeeEntity);
                var developer = employee as T;
                return developer;
            }

            if(employeeEntity.Profession == Profession.Manager)
            {
                employee = Mapper.Map<EmployeeEntity, Manager>(employeeEntity);
                var manager = employee as T;
                return manager;
            }

            employee = Mapper.Map<EmployeeEntity, ServiceWorker>(employeeEntity);
            var serviceWorker = employee as T;
            return serviceWorker;
        }

        public EmployeeEntity GetEmployeeEF<T>(T employee)  where T : Employee
        {
            EmployeeEntity employeeEntity = null;
            
            if (employee is Developer)
            {                     
                var developer = employee as Developer;
                employeeEntity = Mapper.Map<Developer, EmployeeEntity>(developer);
                return employeeEntity;
            }

            if (employee is Manager)
            {
                var manager = employee as Manager;
                employeeEntity = Mapper.Map<Manager, EmployeeEntity>(manager);
                return employeeEntity;
            }

            var serviceWorker = employee as ServiceWorker;
            employeeEntity = Mapper.Map<ServiceWorker, EmployeeEntity>(serviceWorker);

            return employeeEntity;
        }

    }
}
