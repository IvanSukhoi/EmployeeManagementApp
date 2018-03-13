using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
    }
}
