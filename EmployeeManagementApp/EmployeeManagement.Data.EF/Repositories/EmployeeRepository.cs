using AutoMapper;
using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Entities;
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
        private readonly MapperFactory _factotory;
        private readonly IMapper _mapper;

        public EmployeeRepository(EmployeeContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _factotory = new MapperFactory(_mapper);
        }

        public void Create(Employee employee)
        {
            var employeeEntity = _factotory.GetEmployeeEF(employee);
            _dbContext.Employees.Add(employeeEntity);
        }

        public Employee Get(int emoloyeeId)
        {
            var employeeEntity = _dbContext.Employees.FirstOrDefault(x => x.ID == emoloyeeId);
            if (employeeEntity != null)
            {
                var employee = _factotory.GetEmployee<Employee>(employeeEntity);
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
            var employees = _dbContext.Employees.ToList();

            foreach (var i in employees)
            {
                result.Add(_factotory.GetEmployee<Employee>(i));
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
