using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IEmployeeRepository _repository;

        public DeveloperService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public void AddTask(int employeeId, Task task)
        {
            var employee = _repository.Get(employeeId);
            if (employee as Developer != null)
            {
                var developer = employee as Developer;
                developer.Tasks.Add(task);
            }
        }
    }
}
