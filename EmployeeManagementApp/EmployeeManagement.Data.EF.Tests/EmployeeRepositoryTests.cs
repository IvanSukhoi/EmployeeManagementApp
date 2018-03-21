using AutoMapper;
using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Data.EF.Repositories;
using EmployeeManagement.Domain.Models;
using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Data.EF.Tests
{
    public class EmployeeRepositoryTests
    {
        private EmployeeContext _fakeContext;
        private EmployeeRepository _repository;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _fakeContext = DbContextTest.CreateDbContext<EmployeeContext>();
            _mapper = A.Fake<IMapper>();
            _repository = new EmployeeRepository(_fakeContext, _mapper);
        }

        [Test]
        public void Create_Developer_NewEmployeeEntityInContext()
        {
            var developer = new Developer()
            {
                ID = 1,
                ManagerID = 2,
                DepartmentID = 12,
                Sex = Sex.Female,
                FirstName = "Name1",
                MidleName = null,
                LastName = "LastName1",
                Position = Position.Intern,
            };

            var employeeEntity = new EmployeeEntity
            {
                ID = 1,
                ManagerID = 2,
                DepartmentID = 12,
                Sex = Sex.Female,
                FirstName = "Name1",
                MidleName = null,
                LastName = "LastName1",
                Position = Position.Intern,
                Profession = Profession.Developer
            };

            A.CallTo(() => _mapper.Map<Developer, EmployeeEntity>(developer)).Returns(employeeEntity);

            _repository.Create(developer);
            var expectedMappedValue = _fakeContext.Employees.First(x => x.ID == 1);

            AssertObject(developer, expectedMappedValue);
            Assert.That(employeeEntity, Is.EqualTo(_fakeContext.Employees.Last()));
        }

        [Test]
        public void Create_Manager_NewEmployeeEntityInContext()
        {
            var manager = new Manager()
            {
                ID = 1,
                ManagerID = 2,
                DepartmentID = 12,
                Sex = Sex.Female,
                FirstName = "Name1",
                MidleName = null,
                LastName = "LastName1"
            };

            var employeeEntity = new EmployeeEntity()
            {
                ID = 1,
                ManagerID = 2,
                DepartmentID = 12,
                Sex = Sex.Female,
                FirstName = "Name1",
                MidleName = null,
                LastName = "LastName1",
                Profession = Profession.Manager
            };

            A.CallTo(() => _mapper.Map<Manager, EmployeeEntity>(manager)).Returns(employeeEntity);

            _repository.Create(manager);
            var expectedMappedValue = _fakeContext.Employees.First(x => x.ID == 1);

            Assert.That(employeeEntity, Is.EqualTo(_fakeContext.Employees.Last()));
            AssertObject(manager, expectedMappedValue);
        }

        [Test]
        public void Create_ServiceWorker_NewEmployeeEntityInContext()
        {
            var serviceWorker = new ServiceWorker()
            {
                ID = 1,
                ManagerID = 2,
                DepartmentID = 12,
                Sex = Sex.Female,
                FirstName = "Name1",
                MidleName = null,
                LastName = "LastName1", 
                TypeOfWorker = Profession.SystemAdministrator
            };

            var employeeEntity = new EmployeeEntity()
            {
                ID = 1,
                ManagerID = 2,
                DepartmentID = 12,
                Sex = Sex.Female,
                FirstName = "Name1",
                MidleName = null,
                LastName = "LastName1",
                Profession = Profession.SystemAdministrator
            };

            A.CallTo(() => _mapper.Map<ServiceWorker, EmployeeEntity>(serviceWorker)).Returns(employeeEntity);

            _repository.Create(serviceWorker);
            var expectedMappedValue = _fakeContext.Employees.First(x => x.ID == 1);

            Assert.That(employeeEntity, Is.EqualTo(_fakeContext.Employees.Last()));
            AssertObject(serviceWorker, expectedMappedValue);
        }

        [Test]
        public void Delete_EmployeeID_DeleteEmployeeInContext()
        {
            _fakeContext.Employees.Add(new EmployeeEntity()
            {
                ID  = 10
            });

            _repository.Delete(10);

            Assert.That(_fakeContext.Departments.FirstOrDefault(x => x.ID == 10), Is.Null);
        }

        [Test]
        public void Delete_IncorrectEmployeeID_ObjectDisposedException()
        {
            var listEmployeeEntities = new List<EmployeeEntity>();
            _fakeContext.Employees.AddRange(listEmployeeEntities);

            TestDelegate testDelegate = new TestDelegate(() => _repository.Delete(12));

            Assert.That(testDelegate, Throws.TypeOf<ObjectDisposedException>());
        }

        [Test]
        public void Get_DepartmentID_GetDepartment()
        {
            var listEmployeeEntities = new List<EmployeeEntity>();

            var employeeEntity = new EmployeeEntity()
            {
                ID = 12,
                Position = Position.Junior,
                Profession = Profession.Developer
            };

            var employee = new Developer()
            {
                ID = 12,
                Position = Position.Junior
            };

            listEmployeeEntities.Add(employeeEntity);
            _fakeContext.Employees.AddRange(listEmployeeEntities);

            A.CallTo(() => _mapper.Map<EmployeeEntity, Developer>(employeeEntity)).Returns(employee);

            var getDepartment = _repository.Get(12);

            AssertObject(getDepartment, employeeEntity);
            Assert.That(employee, Is.EqualTo(getDepartment));
        }

        [Test]
        public void Get_IncorrectDepartmentID_ObjectDisposedException()
        {
            var listEmployeeEntities = new List<EmployeeEntity>();
            _fakeContext.Employees.AddRange(listEmployeeEntities);

            ActualValueDelegate<object> testDelegate = () => _repository.Get(23);

            Assert.That(testDelegate, Throws.TypeOf<ObjectDisposedException>());
        }

        private void AssertObject(Employee employee, EmployeeEntity expectedMappedValue)
        {
            Assert.That(expectedMappedValue.ID, Is.EqualTo(employee.ID));
            Assert.That(expectedMappedValue.ManagerID, Is.EqualTo(employee.ManagerID));
            Assert.That(expectedMappedValue.DepartmentID, Is.EqualTo(employee.DepartmentID));
            Assert.That(expectedMappedValue.Sex, Is.EqualTo(employee.Sex));
            Assert.That(expectedMappedValue.FirstName, Is.EqualTo(employee.FirstName));
            Assert.That(expectedMappedValue.MidleName, Is.EqualTo(employee.MidleName));
            Assert.That(expectedMappedValue.LastName, Is.EqualTo(employee.LastName));

            if (employee as Developer != null)
            {
                var developer = employee as Developer;
                Assert.That(expectedMappedValue.Position, Is.EqualTo(developer.Position));
                Assert.That(expectedMappedValue.Profession, Is.EqualTo(Profession.Developer));
            }

            if (employee as Manager != null)
            {
                Assert.That(expectedMappedValue.Profession, Is.EqualTo(Profession.Manager));
            }

            if (employee as ServiceWorker != null)
            {
                var serviceWorker = employee as ServiceWorker;
                Assert.That(expectedMappedValue.Profession, Is.EqualTo(serviceWorker.TypeOfWorker));
            }
        }
    }
}
