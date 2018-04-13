using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.DomainServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public void AssignDepartmnet(int departmentId, Employee employee)
        {
            var departmnet = _departmentRepository.Get(departmentId);
            employee.Department = departmnet; 
        }

        public void ChangeDepartment(int departmentId, Employee employee)
        {
            employee.DepartmentID = departmentId;
            var department = _departmentRepository.Get(departmentId);
            employee.Department = department;
        }

        public Employee Get(int employeeId)
        {
            var employee = _employeeRepository.Get(employeeId);
            return employee;
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public void Create(Employee employee)
        {
            _employeeRepository.Create(employee);
        }

        public void Delete(int employeeId)
        {
            _employeeRepository.Delete(employeeId);
        }

        public void Update(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public IEnumerable<Employee> GetByManagerId(int managerId)
        {
           return  _employeeRepository.GetByManagerId(managerId);
        }
    }
}
