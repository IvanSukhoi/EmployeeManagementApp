using AutoMapper;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Domain.Models;
using System;

namespace EmployeeManagement.Data.EF.Mappings
{
    public interface IMapperFactory<T>
    {
        T MappEmployeeEntityToEmployee<T>(EmployeeEntity employeeEntity) where T : Employee;
        EmployeeEntity MappEmployeeToEmployeeEntity<T>(T employee) where T : Employee;
    }
   
    public class MapperFactory : IMapperFactory<Employee>
    {
        private IMapper _mapper;
        public MapperFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T MappEmployeeEntityToEmployee<T>(EmployeeEntity employeeEntity) where T : Employee
        {
            if (employeeEntity.Profession == Profession.Developer)
            {
                var developer = _mapper.Map<EmployeeEntity, Developer>(employeeEntity);
                return developer as T;
            }

            if(employeeEntity.Profession == Profession.Manager)
            {
                var manager = _mapper.Map<EmployeeEntity, Manager>(employeeEntity);
                return manager as T;
            }

            var serviceWorker = _mapper.Map<EmployeeEntity, ServiceWorker>(employeeEntity);
            
            return serviceWorker as T;
        }

        public EmployeeEntity MappEmployeeToEmployeeEntity<T>(T employee)  where T : Employee
        {            
            if (employee.GetType() == typeof(Developer))
            {                     
               return _mapper.Map<Developer, EmployeeEntity>(employee as Developer);
            }

            if (employee.GetType() == typeof(Manager))
            {
                return _mapper.Map<Manager, EmployeeEntity>(employee as Manager);
            }

            if (employee.GetType() == typeof(ServiceWorker))
            {
                return _mapper.Map<ServiceWorker, EmployeeEntity>(employee as ServiceWorker);
            }

            throw new Exception("This type is can not be mapped");
        }
    }
}
