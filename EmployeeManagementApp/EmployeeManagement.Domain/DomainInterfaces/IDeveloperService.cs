using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IDeveloperService
    {
        void AddTask(int employeeId, Task task);
    }
}
