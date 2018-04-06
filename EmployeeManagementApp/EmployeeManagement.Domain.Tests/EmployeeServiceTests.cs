using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Models;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Domain.Tests
{
    public class EmployeeServiceTests
    {
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmnetRepository;

        [SetUp]
        public void SetUp()
        {
            _departmnetRepository = A.Fake<IDepartmentRepository>();
            _employeeRepository = A.Fake<IEmployeeRepository>();
            InitRepositories();
        }

        [Test]
        public void Get_EmployeeID_Employee_GetEmployee()
        {
            var result = _employeeRepository.Get(12);

            Assert.IsNotNull(result);
            Assert.That(result.ID, Is.EqualTo(12));
            Assert.That(result.DepartmentID, Is.EqualTo(19));
            Assert.That(result.Sex, Is.EqualTo(Sex.Female));
            Assert.That(result.FirstName, Is.EqualTo("Name1"));
            Assert.That(result.MidleName, Is.Null);
            Assert.That(result.LastName, Is.EqualTo("LastName1"));
            Assert.That(result.ManagerID, Is.EqualTo(1));
            Assert.That(result.Department, Is.Null);
        }

        [Test]
        public void GetAll_ReturnsAll()
        {
            var result = _employeeRepository.GetAll();

            var expectedValue = result.Find(x => x.ID == 14);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(expectedValue.ID, Is.EqualTo(14));
            Assert.That(expectedValue.DepartmentID, Is.EqualTo(19));
            Assert.That(expectedValue.Sex, Is.EqualTo(Sex.Male));
            Assert.That(expectedValue.FirstName, Is.EqualTo("Name2"));
            Assert.That(expectedValue.MidleName, Is.Null);
            Assert.That(expectedValue.LastName, Is.EqualTo("LastName2"));
            Assert.That(expectedValue.ManagerID, Is.EqualTo(10));
            Assert.That(expectedValue.Department, Is.Null);
        }

        [Test]
        public void ChangeDepartmnet_DepartmnetID_Employee_ChangeDepartmnetEmployee()
        {
            var employee = new Developer();

            var department = _departmnetRepository.Get(19);

            employee.DepartmentID = department.ID;
            employee.Department = department;

            Assert.IsNotNull(department);
            Assert.That(employee.DepartmentID, Is.EqualTo(19));
            Assert.That(employee.Department.ID, Is.EqualTo(19));
            Assert.That(employee.Department.Name, Is.EqualTo("DepName2"));
            //Assert.That(employee.Department.CatalogEmployeeID, Is.EquivalentTo(new[] { 2, 3 }));
        }

        [Test]
        public void AssignDepartment_DepartmentID_Employee_NewDepartmnetEmployee()
        {
            var department = _departmnetRepository.Get(10);
            var employee = new Developer();

            employee.Department = department;

            Assert.IsNotNull(employee.Department);
            //Assert.That(employee.Department.CatalogEmployeeID, Is.EquivalentTo(new[] { 10, 12 }));
            Assert.That(employee.Department.ID, Is.EqualTo(10));
            Assert.That(employee.Department.Name, Is.EqualTo("DepName1"));
        }

        private void InitRepositories()
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
                    Position = Position.Middle,
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

            A.CallTo(() => _employeeRepository.GetAll()).Returns(employees);

            List<Department> departments = new List<Department>
            {
                new Department
                {
                    //CatalogEmployeeID = new List<int>{ 10, 12 },
                    ID = 10,
                    Name = "DepName1"
                },

                new Department
                {
                    //CatalogEmployeeID = new List<int>{ 2, 3 },
                    ID = 19,
                    Name = "DepName2"
                }
            };

            A.CallTo(() => _departmnetRepository.Get(A<int>.Ignored))
                .ReturnsLazily((int id) => departments.FirstOrDefault(_ => _.ID == id));
        }
    }
}
