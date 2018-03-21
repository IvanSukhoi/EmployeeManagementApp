using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Models;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Domain.Tests
{
    public class ManagerServiceTests
    {
        private IEmployeeRepository _employeeRepository;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
            InitEmployeeRepository();
        }

        [Test]
        public void FireEmployee_EmployeeID_DeleteEmployee()
        {
            _employeeRepository.Delete(12);

            Assert.IsNull(_employeeRepository.Get(12));
        }

        [Test]
        public void Promote_EmployeeID_Position_NewPositionDeveloper()
        {
            var employee = _employeeRepository.Get(12);

            var developer = employee as Developer;
            developer.Position = Position.Senior;

            Assert.IsNotNull(developer);
            Assert.That(developer.Position, Is.EqualTo(developer.Position));
        }

        [Test]
        public void RecruitEmployee_Employee_AddEmployee()
        {
            var developer = new Developer
            {
                ID = 21,
                DepartmentID = 23,
                Sex = Sex.Female,
                FirstName = "Name3",
                MidleName = null,
                LastName = "LastName3",
                ManagerID = 3,
                Position = Position.Senior,
                Department = null
            };

            _employeeRepository.Create(developer);

            var expectedValue = _employeeRepository.Get(21);

            Assert.IsNotNull(expectedValue);
            Assert.That(expectedValue.ID, Is.EqualTo(21));
            Assert.That(expectedValue.DepartmentID, Is.EqualTo(23));
            Assert.That(expectedValue.Sex, Is.EqualTo(Sex.Female));
            Assert.That(expectedValue.FirstName, Is.EqualTo("Name3"));
            Assert.That(expectedValue.MidleName, Is.Null);
            Assert.That(expectedValue.LastName, Is.EqualTo("LastName3"));
            Assert.That(expectedValue.ManagerID, Is.EqualTo(3));
            Assert.That(expectedValue.Department, Is.Null);
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
                    Department  = null
                },

                new Manager
                {
                    ID = 14,
                    DepartmentID = 19,
                    Sex = Sex.Male,
                    FirstName = "Name2",
                    MidleName = null,
                    LastName = "LastName2",
                    ManagerID = 10,
                    Department = null
                }
            };

            A.CallTo(() => _employeeRepository.Get(A<int>.Ignored))
                .ReturnsLazily((int id) => employees.FirstOrDefault(_ => _.ID == id));

            A.CallTo(() => _employeeRepository.Create(A<Employee>.Ignored)).Invokes((Employee emp) => employees.Add(emp));

            A.CallTo(() => _employeeRepository.Delete(A<int>.Ignored)).Invokes((int id) => employees.Remove(employees.First(x => x.ID == id)));
        }
    }
}
