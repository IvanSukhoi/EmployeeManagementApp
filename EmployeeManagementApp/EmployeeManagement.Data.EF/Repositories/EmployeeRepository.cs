using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Data.EF.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _dbContext;
        private readonly MapperFactory _factotory;

        public EmployeeRepository(EmployeeContext dbContext)
        {
            _dbContext = dbContext;
            _factotory = new MapperFactory();
        }

        public void Create(Employee employee)
        {
            var employeeEntity = _factotory.GetEmployeeEF(employee);
            _dbContext.Employees.Add(employeeEntity);
        }

        public Employee Get(int emoloyeeId)
        {
            var employeeEntity = _dbContext.Employees.FirstOrDefault(x => x.ID == emoloyeeId);
            var employee = _factotory.GetEmployee<Employee>(employeeEntity);
            return employee;
           
        }

        public List<Employee> GetAll()
        {
            var employees = new List<Employee>();
            foreach(var i in _dbContext.Employees.ToList())
            {
                employees.Add(_factotory.GetEmployee<Employee>(i));
            }
            return employees;
        }

        public void Delete(int employeeId)
        { 
            EmployeeEntity employeeEntity = _dbContext.Employees.FirstOrDefault(x => x.ID == employeeId);
            if (employeeEntity != null)
                _dbContext.Employees.Remove(employeeEntity);
        }

        public void Update(Employee employee)
        { 

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
