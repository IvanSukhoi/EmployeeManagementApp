using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class WorkerService : IWorkerService
    {
        private readonly IEmployeeRepository _repository;

        public WorkerService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public void ChangeProfession(int employeeId, Profession profession)
        {
            var employee = _repository.Get(employeeId);

            if (employee as ServiceWorker != null)
            {
                var serviceWorker = employee as ServiceWorker;
                serviceWorker.TypeOfWorker = profession;
            }
        }
    }
}
