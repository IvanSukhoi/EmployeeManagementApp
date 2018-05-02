using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Models;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Domain.Tests
{
    public class ServiceWorkerTests
    {
        private IEmployeeRepository _employeeRepository;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            InitEmployeeRepository();
        }

        [Test]
        public void ChangeProfession_EmployeeID_Profession_ChangeTypeOfWorker()
        {
            var employee = _employeeRepository.Get(12);

            var serviceWorker = employee as ServiceWorker;
            serviceWorker.TypeOfWorker = Profession.Designer;

            Assert.IsNotNull(serviceWorker);
            Assert.That(serviceWorker.TypeOfWorker, Is.EqualTo(Profession.Designer));
        }

        private void InitEmployeeRepository()
        {
            List<Employee> employees = new List<Employee>
            {
                new ServiceWorker
                {
                    ID = 12,
                    DepartmentID = 19,
                    Sex = Sex.Female,
                    FirstName = "Name1",
                    MiddleName = null,
                    LastName = "LastName1",
                    ManagerID = 1,
                    Department  = null,
                    TypeOfWorker = Profession.Designer
                },
            };

            A.CallTo(() => _employeeRepository.Get(A<int>.Ignored))
                .ReturnsLazily((int id) => employees.FirstOrDefault(_ => _.ID == id));
        }
    }
}
