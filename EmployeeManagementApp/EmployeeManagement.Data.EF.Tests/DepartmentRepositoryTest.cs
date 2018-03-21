using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Data.EF.Repositories;
using FakeItEasy;
using NUnit.Framework;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using EmployeeManagement.Domain.Models;
using System.Collections.Generic;
using System;
using NUnit.Framework.Constraints;

namespace EmployeeManagement.Data.EF.Tests
{
    public class DepartmentRepositoryTest
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
        public void GetAll_ReturnsAll()
        {
            //arrange
            List<DepartmentEntity> departmentEntity = Enumerable.Repeat(new DepartmentEntity(), 10).ToList();
            _fakeContext.Departments.AddRange(departmentEntity.ToArray());

            var departments = Enumerable.Repeat(new Department(), 10).ToList();

            A.CallTo(() => _mapper.Map<List<DepartmentEntity>, List<Department>>(A<List<DepartmentEntity>>.Ignored))
                   .ReturnsLazily(() => departments.Take(departmentEntity.Count).ToList());

            //act
            var actual = _repository.GetAll();

            //assert
            Assert.AreEqual(10, actual.Count());
        }

        [Test]
        public void Map_NoMappedObject_MappedObject()
        {
            //arrange
            List<DepartmentEntity> listDepartmentEntity = new List<DepartmentEntity>();
            List<Department> listDepartment = new List<Department>();

            var listEmployeeID = new List<int>(new int[] { 1, 2 ,3});

            listDepartmentEntity.Add(new DepartmentEntity() { EmployeeID = listEmployeeID, ID = 5, Name = "HR" });
            listDepartmentEntity.Add(new DepartmentEntity() { EmployeeID = listEmployeeID, ID = 8, Name = "Bookkeeping" });

            listDepartment.Add(new Department() { CatalogEmployeeID = listEmployeeID, ID = 5, Name = "HR" });
            listDepartment.Add(new Department() { CatalogEmployeeID = listEmployeeID, ID = 8, Name = "Bookkeeping" });

            _fakeContext.Departments.AddRange(listDepartmentEntity.ToArray());

            A.CallTo(() => _mapper.Map<List<DepartmentEntity>, List<Department>>(A<List<DepartmentEntity>>.Ignored)).Returns(listDepartment);
            _fakeContext.Departments.AddRange(listDepartmentEntity.ToArray());

            //act
            var actual = _repository.GetAll();

            //assert
            Assert.AreEqual(actual.First().CatalogEmployeeID, listDepartment.First().CatalogEmployeeID);
            Assert.AreEqual(actual.First().ID, listDepartment.First().ID);
            Assert.AreEqual(actual.First().Name, listDepartment.First().Name);

            Assert.AreEqual(actual.Last().CatalogEmployeeID, listDepartment.Last().CatalogEmployeeID);
            Assert.AreEqual(actual.Last().ID, listDepartment.Last().ID);
            Assert.AreEqual(actual.Last().Name, listDepartment.Last().Name);

            Assert.AreEqual(actual.First(), listDepartment.First());
            Assert.AreEqual(actual.Last(), listDepartment.Last());
        }
        
        [Test]
        public void Create_Department_NewDepartmentEntityInContext()
        {
            //arrange
            var department = new Department() { CatalogEmployeeID = new List<int>(new int[] {1, 2}) , ID = 5, Name = "HR" };
            var departmentEntity = new DepartmentEntity() { EmployeeID = new List<int> (new int[]{ 1, 2}), ID = 10, Name = "HR" };
            A.CallTo(() => _mapper.Map<Department, DepartmentEntity>(department)).Returns(departmentEntity);

            //act
            _repository.Create(department);

            //assert
            Assert.AreEqual(departmentEntity, _fakeContext.Departments.Last());
        }

        [Test]
        public void Delete_DepartmnetID_DeleteDepartmentInContext()
        {
            //arrange
            _fakeContext.Departments.Add(new DepartmentEntity() { EmployeeID = null, ID = 10, Name = "HR" });

            //act
            _repository.Delete(10);

            //assert
            Assert.That(_fakeContext.Departments.FirstOrDefault(x => x.ID == 10), Is.Null);
        }

        [Test]
        public void Delete_IncorrectDepartmnetID_WithoutChanges()
        {
            //arrange
            _fakeContext.Departments.Add(new DepartmentEntity() { EmployeeID = null, ID = 10, Name = "HR" });
            _fakeContext.Departments.Add(new DepartmentEntity() { EmployeeID = null, ID = 14, Name = "Bookkeping" });

            //act
            TestDelegate testDelegate = new TestDelegate(() => _repository.Delete(12));

            //assert
            Assert.That(testDelegate, Throws.TypeOf<ObjectDisposedException>());
        }

        [Test]
        public void Get_DepartmentID_GetDepartment()
        {
            //arrange
            var listDepartmentEntity = new List<DepartmentEntity>();
            var departmentEntity = new DepartmentEntity() { EmployeeID = null, ID = 10, Name = "HR" };
            var department = new Department() { CatalogEmployeeID = null, ID = 15, Name = "HR" };

            listDepartmentEntity.Add(departmentEntity);
            listDepartmentEntity.AddRange(Enumerable.Repeat(new DepartmentEntity(), 10));
            _fakeContext.Departments.AddRange(listDepartmentEntity.ToArray());

            A.CallTo(() => _mapper.Map<DepartmentEntity, Department>(departmentEntity)).Returns(department);

            //act
            Department getDepartment = _repository.Get(10);

            //assert
            Assert.AreEqual(department, getDepartment);
        }

        [Test]
        public void Get_IncorrectDepartmentID_ObjectDisposedException()
        {
            //arrange
            var listDepartmentEntity = new List<DepartmentEntity>();
            _fakeContext.Departments.AddRange(listDepartmentEntity.ToArray());

            //act
            ActualValueDelegate<object> testDelegate = () => _repository.Get(23);

            //assert
            Assert.That(testDelegate, Throws.TypeOf<ObjectDisposedException>());
        }

        [Test]
        public void Update_NotImplementedExcepton()
        {
            TestDelegate testDelegate = new TestDelegate(() => _repository.Update(new Department()));
            Assert.That(testDelegate, Throws.TypeOf<NotImplementedException>());
        }
    }
}