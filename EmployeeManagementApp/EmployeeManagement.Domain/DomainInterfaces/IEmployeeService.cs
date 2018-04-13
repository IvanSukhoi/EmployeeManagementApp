using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee Get(int employeeId);
        void AssignDepartmnet(int departmentId, Employee employee);
        void ChangeDepartment(int departmnetId, Employee employee);
        void Update(Employee employee);
        void Create(Employee employee);
        void Delete(int employeeId);
        IEnumerable<Employee> GetByManagerId(int managerId);
    }
}
