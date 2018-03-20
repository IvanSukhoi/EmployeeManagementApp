using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class ManagerService : IManagerService
    {
        private readonly IEmployeeRepository _repository;

        public ManagerService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public void AddEmployee(Employee employee)
        {
            _repository.Create(employee);
        }

        public void FireEmployee(int employeeId)
        {
            _repository.Delete(employeeId);
        }

        public void Promote(int employeeId, Position position)
        {
            var employee = _repository.Get(employeeId);

            if (employee as Developer != null)
            {
                var developer = employee as Developer;
                developer.Position = position; 
            }
        }
    }
}
