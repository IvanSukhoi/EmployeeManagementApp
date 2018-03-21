using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class WorkerService : IWorkerService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public WorkerService(IEmployeeRepository repository)
        {
            _employeeRepository = repository;
        }

        public void ChangeProfession(int employeeId, Profession profession)
        {
            var employee = _employeeRepository.Get(employeeId);

            if (employee as ServiceWorker != null)
            {
                var serviceWorker = employee as ServiceWorker;
                serviceWorker.TypeOfWorker = profession;
            }
        }
    }
}
