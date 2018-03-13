using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.DomainServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }
    }
}
