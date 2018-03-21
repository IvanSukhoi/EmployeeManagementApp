using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IManagerService
    {
        void FireEmployee(int employeeId);
        void RecruitEmployee(Employee employee);
        void Promote(int employeeId, Position position);
    }
}
