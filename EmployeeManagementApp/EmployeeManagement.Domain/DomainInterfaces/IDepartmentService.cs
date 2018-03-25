using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department Get(int employeeId);
        void Create(Department department);
        void Update(Department department);
        void Delete(int departmentId);
    }
}
