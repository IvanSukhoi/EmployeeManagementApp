using AutoMapper;
using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Data.EF.Mappings;
using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Data.EF.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _dbContext;
        private readonly IMapperFactory<Employee> _factory;

        public EmployeeRepository(EmployeeContext dbContext, IMapperFactory<Employee> factory)
        {
            _dbContext = dbContext;
            _factory = factory;
        }

        public void Create(Employee employee)
        {
            var employeeEntity = _factory.GetEmployeeEF(employee);
            _dbContext.Employees.Add(employeeEntity);
        }

        public Employee Get(int emoloyeeId)
        {
            var employeeEntity = _dbContext.Employees.FirstOrDefault(x => x.ID == emoloyeeId);
            if (employeeEntity != null)
            {
                var employee = _factory.GetEmployee<Employee>(employeeEntity);

                if (employee as Manager != null)
                {
                    var manager = employee as Manager;
                    manager.EmployeeID = _dbContext.Employees.Where(x => x.ID == manager.ID).Select(x => x.ID).ToList();

                    return manager;
                }

                return employee;
            }
            else
            {
                throw new ObjectDisposedException("Object with such Id was not found");
            }
        }

        public List<Employee> GetAll()
        {
            var result = new List<Employee>();
            var employeeEntities = _dbContext.Employees.ToList();

            foreach (var employeeEntity in employeeEntities)
            {
                var employee = _factory.GetEmployee<Employee>(employeeEntity);

                if (employee as Manager != null)
                {
                    var manager = employee as Manager;
                    manager.EmployeeID = _dbContext.Employees.Where(x => x.ManagerID == manager.ID).Select(x => x.ID).ToList();

                    result.Add(manager);
                }
                else
                {
                    result.Add(employee);
                }
            }

            return result;
        }

        public void Delete(int employeeId)
        {
            EmployeeEntity employeeEntity = _dbContext.Employees.FirstOrDefault(x => x.ID == employeeId);
            if (employeeEntity != null)
            {
                _dbContext.Employees.Remove(employeeEntity);
            }
            else
            {
                throw new ObjectDisposedException("Object with such Id was not found");
            }
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
