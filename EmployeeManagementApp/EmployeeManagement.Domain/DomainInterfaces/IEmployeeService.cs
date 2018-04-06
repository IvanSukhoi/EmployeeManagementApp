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
        void SaveEmployee(Employee employee);
    }
}
