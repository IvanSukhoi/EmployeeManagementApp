using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Models;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Domain.Tests
{
    public class DeveloperServiceTests
    {
        private IEmployeeRepository _employeeRepository;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            InitEmployeeRepository();
        }

        [Test]
        public void AddTask_EmployeeID_Task_NewTaskAtDeveloper()
        {
            var employee = _employeeRepository.Get(12);
            var task = new Task
            {
                Name = "NameTask",
                TaskID = 10
            };

            var developer = employee as Developer;

            developer.Tasks.Add(task);

            Assert.IsNotNull(developer);
            Assert.IsNotNull(developer.Tasks.FirstOrDefault(x => x.TaskID == 10));
        }

        private void InitEmployeeRepository()
        {
            List<Employee> employees = new List<Employee>
            {
                new Developer
                {
                    ID = 12,
                    DepartmentID = 19,
                    Sex = Sex.Female,
                    FirstName = "Name1",
                    MidleName = null,
                    LastName = "LastName1",
                    ManagerID = 1,
                    Position = Position.Midle,
                    Department  = null,
                    Tasks = new List<Task>()
                },
            };

            A.CallTo(() => _employeeRepository.Get(A<int>.Ignored))
                .ReturnsLazily((int id) => employees.FirstOrDefault(_ => _.ID == id));
        }
    }
}

