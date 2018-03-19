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
    public class EmployeeRepositoryTest
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
            //arrange
            var developer = new Developer();

            var employeeEntity = new EmployeeEntity()
            {
                Profession = Profession.Developer
            };

            A.CallTo(() => _mapper.Map<Developer, EmployeeEntity>(developer)).Returns(employeeEntity);

            //act
            _repository.Create(developer);

            //assert
            Assert.AreEqual(employeeEntity, _fakeContext.Employees.Last());
        }

        [Test]
        public void Create_Manager_NewEmployeeEntityInContext()
        {
            //arrange
            var manager = new Manager();

            var employeeEntity = new EmployeeEntity()
            {
                Profession = Profession.Manager
            };

            A.CallTo(() => _mapper.Map<Manager, EmployeeEntity>(manager)).Returns(employeeEntity);

            //act
            _repository.Create(manager);

            //assert
            Assert.AreEqual(employeeEntity, _fakeContext.Employees.Last());
        }

        [Test]
        public void Create_ServiceWorker_NewEmployeeEntityInContext()
        {
            //arrange
            var serviceWorker = new ServiceWorker();

            var employeeEntity = new EmployeeEntity()
            {
                Profession = Profession.Bookkeeper
            };

            A.CallTo(() => _mapper.Map<ServiceWorker, EmployeeEntity>(serviceWorker)).Returns(employeeEntity);

            //act
            _repository.Create(serviceWorker);

            //assert
            Assert.AreEqual(employeeEntity, _fakeContext.Employees.Last());
        }

        [Test]
        public void GetAll_ReturnsAll()
        {
            //arrange
            List<EmployeeEntity> employeeEntity = employeeEntity = Enumerable.Repeat(new EmployeeEntity(), 10).ToList();
            _fakeContext.Employees.AddRange(employeeEntity.ToArray());

            A.CallTo(() => _mapper.Map<EmployeeEntity, Developer>(A<EmployeeEntity>.Ignored))
                   .Returns(new Developer());

            A.CallTo(() => _mapper.Map<EmployeeEntity, Manager>(A<EmployeeEntity>.Ignored))
                   .Returns(new Manager());

            A.CallTo(() => _mapper.Map<EmployeeEntity, ServiceWorker>(A<EmployeeEntity>.Ignored))
                    .Returns(new ServiceWorker());

            //act
            var actual = _repository.GetAll();

            //assert
            Assert.AreEqual(10, actual.Count());
        }

        [Test]
        public void Map_NoMappedObject_MappedObject()
        {
            //arrange
            List<EmployeeEntity> listEmployeeEntity = new List<EmployeeEntity>();

            listEmployeeEntity.Add(new EmployeeEntity()
            {
                Department = new DepartmentEntity(),
                DepartmentID = 10,
                ID = 1,
                FirstName = "Artem",
                MidleName = null,
                LastName = "Ivanov",
                ManagerID = 23,
                Position = Position.Intern,
                Sex = Sex.Female,
                Profession = Profession.Developer
            });

            listEmployeeEntity.Add(new EmployeeEntity()
            {
                Department = new DepartmentEntity(),
                DepartmentID = 11,
                ID = 3,
                FirstName = "Oleg",
                MidleName = null,
                LastName = "Mironov",
                ManagerID = 23,
                Sex = Sex.Female,
                Profession = Profession.Manager
            });

            listEmployeeEntity.Add(new EmployeeEntity()
            {
                Department = new DepartmentEntity(),
                DepartmentID = 14,
                ID = 8,
                FirstName = "Inna",
                MidleName = null,
                LastName = "Aleksandrova",
                ManagerID = 50,
                Sex = Sex.Male,
                Profession = Profession.Bookkeeper
            });

            List<Employee> listEmployee = new List<Employee>();
            listEmployee.Add(new Developer()
            {
                Department = new Department(),
                DepartmentID = 10,
                ID = 1,
                FirstName = "Artem",
                MidleName = null,
                LastName = "Ivanov",
                ManagerID = 23,
                Position = Position.Intern,
                Sex = Sex.Female,
                Tasks = null
            });

            listEmployee.Add(new Manager()
            {
                Department = new Department(),
                DepartmentID = 11,
                ID = 3,
                FirstName = "Oleg",
                MidleName = null,
                LastName = "Mironov",
                ManagerID = 23,
                Sex = Sex.Female,
                Employees = null,
            });

            listEmployee.Add(new ServiceWorker()
            {
                Department = new Department(),
                DepartmentID = 14,
                ID = 8,
                FirstName = "Inna",
                MidleName = null,
                LastName = "Aleksandrova",
                ManagerID = 50,
                Sex = Sex.Male,
                TypeOfWorker = Profession.Bookkeeper
            });

            A.CallTo(() => _mapper.Map<DepartmentEntity, Department>(A<DepartmentEntity>.Ignored)).Returns(new Department());
            _fakeContext.Employees.AddRange(listEmployeeEntity.ToArray());

            for (int i = 0; i < listEmployeeEntity.Count; i++)
            {
                var employee = listEmployeeEntity.ElementAt(i);
                if (employee.Profession == Profession.Developer)
                {
                    var developer = listEmployee.ElementAt(i) as Developer;
                    A.CallTo(() => _mapper.Map<EmployeeEntity, Developer>(employee)).Returns(developer);
                }

                if (employee.Profession == Profession.Manager)
                {
                    var manager = listEmployee.ElementAt(i) as Manager;
                    A.CallTo(() => _mapper.Map<EmployeeEntity, Manager>(employee)).Returns(manager);
                }

                if (employee.Profession != Profession.Developer && employee.Profession != Profession.Manager)
                {
                    var serviceWorker = listEmployee.ElementAt(i) as ServiceWorker;
                    A.CallTo(() => _mapper.Map<EmployeeEntity, ServiceWorker>(employee)).Returns(serviceWorker);
                }
            }

            //act
            var actual = _repository.GetAll();

            //assert
            for (int i = 0; i < actual.Count(); i++)
            {
                var employee = actual.ElementAt(i);
                var employeeEntity = listEmployeeEntity.ElementAt(i);

                Assert.AreEqual(employee.DepartmentID, employeeEntity.DepartmentID);
                Assert.AreEqual(employee.FirstName, employeeEntity.FirstName);
                Assert.AreEqual(employee.ID, employeeEntity.ID);
                Assert.AreEqual(employee.LastName, employeeEntity.LastName);
                Assert.AreEqual(employee.MidleName, employeeEntity.MidleName);
                Assert.AreEqual(employee.Sex, employeeEntity.Sex);
                Assert.AreEqual(employee.ManagerID, employeeEntity.ManagerID);

                if (employee as Developer != null)
                {
                    var developer = employee as Developer;
                    Assert.AreEqual(developer.Position, employeeEntity.Position);
                    Assert.AreEqual(developer.Tasks, null);
                }

                if (employee as Manager != null)
                {
                    var manager = employee as Manager;
                    Assert.AreEqual(manager.Employees, null);
                }

                if (employee as ServiceWorker != null)
                {
                    var serviceWorker = employee as ServiceWorker;
                    Assert.AreEqual(serviceWorker.TypeOfWorker, employeeEntity.Profession);
                }
            }
        }

        [Test]
        public void Delete_EmployeeID_DeleteEmployeeInContext()
        {
            //arrange
            _fakeContext.Employees.Add(new EmployeeEntity()
            {
                ID  = 10
            });

            //act
            _repository.Delete(10);

            //assert
            Assert.That(_fakeContext.Departments.FirstOrDefault(x => x.ID == 10), Is.Null);
        }

        [Test]
        public void Delete_IncorrectEmployeeID_ObjectDisposedException()
        {
            //arrange
            var listEmployeeEntity = new List<EmployeeEntity>();
            _fakeContext.Employees.AddRange(listEmployeeEntity.ToArray());

            //act
            TestDelegate testDelegate = new TestDelegate(() => _repository.Delete(12));

            //assert
            Assert.That(testDelegate, Throws.TypeOf<ObjectDisposedException>());
        }

        [Test]
        public void Get_DepartmentID_GetDepartment()
        {
            //arrange
            var listEmployeeEntity = new List<EmployeeEntity>();

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

            listEmployeeEntity.Add(employeeEntity);
            listEmployeeEntity.AddRange(Enumerable.Repeat(new EmployeeEntity(), 10));
            _fakeContext.Employees.AddRange(listEmployeeEntity.ToArray());

            A.CallTo(() => _mapper.Map<EmployeeEntity, Developer>(employeeEntity)).Returns(employee);

            //act
            var getDepartment = _repository.Get(12);

            //assert
            Assert.AreEqual(employee, getDepartment);
        }

        [Test]
        public void Get_IncorrectDepartmentID_ObjectDisposedException()
        {
            //arrange
            var listEmployeeEntity = new List<EmployeeEntity>();
            _fakeContext.Employees.AddRange(listEmployeeEntity.ToArray());

            //act
            ActualValueDelegate<object> testDelegate = () => _repository.Get(23);

            //assert
            Assert.That(testDelegate, Throws.TypeOf<ObjectDisposedException>());
        }

        [Test]
        public void Update_NotImplementedExcepton()
        {
            TestDelegate testDelegate = new TestDelegate(() => _repository.Update(new Developer()));
            Assert.That(testDelegate, Throws.TypeOf<NotImplementedException>());
        }
    }
}
