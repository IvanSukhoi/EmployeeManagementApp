using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeveloperService(IEmployeeRepository repository)
        {
            _employeeRepository = repository;
        }

        public void AddTask(int employeeId, Task task)
        {
            var employee = _employeeRepository.Get(employeeId);
            if (employee as Developer != null)
            {
                var developer = employee as Developer;
                developer.Tasks.Add(task);
            }
        }
    }
}
