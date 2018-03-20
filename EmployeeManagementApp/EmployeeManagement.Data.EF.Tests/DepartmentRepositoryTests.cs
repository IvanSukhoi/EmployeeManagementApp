using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Data.EF.Repositories;
using FakeItEasy;
using NUnit.Framework;
using System.Linq;
using AutoMapper;
using EmployeeManagement.Domain.Models;
using System.Collections.Generic;
using System;

namespace EmployeeManagement.Data.EF.Tests
{
    public class DepartmentRepositoryTests
    {
        private EmployeeContext _fakeContext;
        private DepartmentRepository _repository;
        private IMapper _mapper;      

        [SetUp]
        public void SetUp()
        {
            _fakeContext = DbContextTest.CreateDbContext<EmployeeContext>();
            _mapper = A.Fake<IMapper>();
            _repository = new DepartmentRepository(_fakeContext, _mapper);
        }
        
        [Test]
        public void Create_Department_NewDepartmentEntityInContext()
        {
            var departmentEntity = new DepartmentEntity
            {
                CatalogEmployeeID = new List<int>{1, 2},
                ID = 5,
                Name = "HR"
            };

            var department = new Department
            {
                CatalogEmployeeID = new List<int>{1, 2},
                ID = 5,
                Name = "HR"
            };

            A.CallTo(() => _mapper.Map<Department, DepartmentEntity>(department)).Returns(departmentEntity);

            _repository.Create(department);
            var expectedMappedValue = _fakeContext.Departments.First(x => x.ID == 5);

            Assert.That(departmentEntity, Is.EqualTo(_fakeContext.Departments.Last()));
            Assert.That(expectedMappedValue.ID, Is.EqualTo(5));
            Assert.That(expectedMappedValue.Name, Is.EqualTo("HR"));
            Assert.That(expectedMappedValue.CatalogEmployeeID, Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test]
        public void Delete_DepartmnetID_DeleteDepartmentInContext()
        {
            _fakeContext.Departments.Add(new DepartmentEntity() { CatalogEmployeeID = null, ID = 10, Name = "HR" });

            _repository.Delete(10);

            Assert.That(_fakeContext.Departments.FirstOrDefault(x => x.ID == 10), Is.Null);
        }

        [Test]
        public void Delete_IncorrectDepartmnetID_ObjectDisposedException()
        {
            var listDepartmentEntities = new List<DepartmentEntity>();
            _fakeContext.Departments.AddRange(listDepartmentEntities);

            TestDelegate testDelegate = new TestDelegate(() => _repository.Delete(12));

            Assert.That(testDelegate, Throws.TypeOf<ObjectDisposedException>());
        }

        [Test]
        public void Get_DepartmentID_GetDepartment()
        {
            var listDepartmentEntities = new List<DepartmentEntity>();

            var departmentEntity = new DepartmentEntity
            {
                CatalogEmployeeID = new List<int> {10, 12},
                ID = 15,
                Name = "HR"
            };

            var department = new Department
            {
                CatalogEmployeeID = new List<int> {10, 12},
                ID = 15,
                Name = "HR"
            };

            listDepartmentEntities.Add(departmentEntity);
            _fakeContext.Departments.AddRange(listDepartmentEntities);

            A.CallTo(() => _mapper.Map<DepartmentEntity, Department>(departmentEntity)).Returns(department);

            Department getDepartment = _repository.Get(10);
            var expectedMappedValue = _fakeContext.Departments.First(x => x.ID == 15);

            Assert.That(department, Is.EqualTo(getDepartment));
            Assert.That(expectedMappedValue.ID, Is.EqualTo(15));
            Assert.That(expectedMappedValue.Name, Is.EqualTo("HR"));
            Assert.That(expectedMappedValue.CatalogEmployeeID, Is.EquivalentTo(new[] { 10, 12 }));
        }

        [Test]
        public void Get_IncorrectDepartmentID_ObjectDisposedException()
        {
            var listDepartmentEntities = new List<DepartmentEntity>();
            _fakeContext.Departments.AddRange(listDepartmentEntities);

            TestDelegate testDelegate = () => _repository.Get(23);

            Assert.That(testDelegate, Throws.TypeOf<ObjectDisposedException>());
        }
    }
}