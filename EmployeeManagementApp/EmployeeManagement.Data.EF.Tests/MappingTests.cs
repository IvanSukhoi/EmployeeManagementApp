using AutoMapper;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Data.EF.Mapp;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Mappings;
using NUnit.Framework;
using System.Collections.Generic;

namespace EmployeeManagement.Data.EF.Tests
{
    public class MappingTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile(new DataMappingProfile());
                c.AddProfile(new UIMappingProfile());
            }).CreateMapper();
        }

        [Test]
        public void AutoMapper_ConvertFromDepartmentEntityInDepartnet_IsValid()
        {
            var departmentEntity = new DepartmentEntity
            {
                CatalogEmployeeID = new List<int> { 1, 3 },
                ID = 3,
                Name = "DepEntityName1"
            };

            var departmnet = _mapper.Map<DepartmentEntity, Department>(departmentEntity);

            Assert.That(departmentEntity.CatalogEmployeeID, Is.EquivalentTo(departmnet.CatalogEmployeeID));
            Assert.That(departmentEntity.ID, Is.EqualTo(departmnet.ID));
            Assert.That(departmentEntity.Name, Is.EqualTo(departmnet.Name));
        }

        [Test]
        public void AutoMapper_ConvertFromDepartmentInDepartnetEntity_IsValid()
        {
            var department = new Department
            {
                CatalogEmployeeID = new List<int> { 1, 3 },
                ID = 3,
                Name = "DepName1"
            };

            var departmnetEntity = _mapper.Map<Department, DepartmentEntity>(department);

            Assert.That(department.CatalogEmployeeID, Is.EquivalentTo(departmnetEntity.CatalogEmployeeID));
            Assert.That(department.ID, Is.EqualTo(departmnetEntity.ID));
            Assert.That(department.Name, Is.EqualTo(departmnetEntity.Name));
        }

        [Test]
        public void AutoMapper_ConvertFromEmployeeEntityInDeveloper_IsValid()
        {
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

            var employee = _mapper.Map<EmployeeEntity, Developer>(employeeEntity);

            AssertPropertyValue(employee, employeeEntity);
            Assert.That(employeeEntity.Position, Is.EqualTo(employee.Position));
            Assert.That(employeeEntity.Profession, Is.EqualTo(Profession.Developer));
        }

        [Test]
        public void AutoMapper_ConvertFromEmployeeEntityInServiceWorker_IsValid()
        {
            var employeeEntity = new EmployeeEntity
            {
                ID = 5,
                ManagerID = 8,
                DepartmentID = 19,
                Sex = Sex.Male,
                FirstName = "Name2",
                MidleName = null,
                LastName = "LastName2",
                Position = Position.Intern,
                Profession = Profession.Designer
            };

            var employee = _mapper.Map<EmployeeEntity, ServiceWorker>(employeeEntity);

            AssertPropertyValue(employee, employeeEntity);
            Assert.That(employee.TypeOfWorker, Is.EqualTo(employeeEntity.Profession));
        }

        [Test]
        public void AutoMapper_ConvertFromEmployeeEntityInManager_IsValid()
        {
            var employeeEntity = new EmployeeEntity
            {
                ID = 10,
                ManagerID = 10,
                DepartmentID = 19,
                Sex = Sex.Female,
                FirstName = "Name3",
                MidleName = null,
                LastName = "LastName3",
                Position = Position.Intern,
                Profession = Profession.Manager
            };

            var employee = _mapper.Map<EmployeeEntity, Manager>(employeeEntity);

            AssertPropertyValue(employee, employeeEntity);
            Assert.That(employeeEntity.Profession, Is.EqualTo(Profession.Manager));
            Assert.IsNull(employee.EmployeeID);
        }

        [Test]
        public void Automapper_ConvertFromDeveloperInEmployeeEntity_IsValid()
        {
            var developer = new Developer
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

            var employeeEntity = _mapper.Map<Developer, EmployeeEntity>(developer);

            AssertPropertyValue(developer, employeeEntity);
            Assert.That(employeeEntity.Position, Is.EqualTo(developer.Position));
            Assert.That(employeeEntity.Profession, Is.EqualTo(Profession.Developer));
        }

        [Test]
        public void Automapper_ConvertFromManagerInEmployeeEntity_IsValid()
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

            var employeeEntity = _mapper.Map<Manager, EmployeeEntity>(manager);

            AssertPropertyValue(manager, employeeEntity);
            Assert.That(employeeEntity.Profession, Is.EqualTo(Profession.Manager));
            Assert.IsNull(manager.EmployeeID);
        }

        [Test]
        public void Automapper_ConvertFromServiceWorkerInEmployeeEntity_IsValid()
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

            var employeeEntity = _mapper.Map<ServiceWorker, EmployeeEntity>(serviceWorker);

            AssertPropertyValue(serviceWorker, employeeEntity);
            Assert.That(employeeEntity.Profession, Is.EqualTo(serviceWorker.TypeOfWorker));
        }

        private void AssertPropertyValue(Employee employee, EmployeeEntity employeeEntity)
        {
            Assert.That(employeeEntity.ID, Is.EqualTo(employee.ID));
            Assert.That(employeeEntity.ManagerID, Is.EqualTo(employee.ManagerID));
            Assert.That(employeeEntity.DepartmentID, Is.EqualTo(employee.DepartmentID));
            Assert.That(employeeEntity.Sex, Is.EqualTo(employee.Sex));
            Assert.That(employeeEntity.FirstName, Is.EqualTo(employee.FirstName));
            Assert.That(employeeEntity.MidleName, Is.EqualTo(employee.MidleName));
            Assert.That(employeeEntity.LastName, Is.EqualTo(employee.LastName));
        }
    }
}