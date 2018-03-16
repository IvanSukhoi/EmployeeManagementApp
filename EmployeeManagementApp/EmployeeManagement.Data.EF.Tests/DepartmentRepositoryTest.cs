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
            List<DepartmentEntity> departmentEntity = departmentEntity = Enumerable.Repeat(new DepartmentEntity(), 10).ToList();
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
            List<DepartmentEntity> listDepartmntEntity = new List<DepartmentEntity>();
            List<Department> listDepartment = new List<Department>();

            var listEmployeeID1 = new List<int>(new int[] {1, 2});
            var listEmployeeID2 = new List<int>(new int[] { 3, 4, 5 });

            listDepartmntEntity.Add(new DepartmentEntity() { EmployeeID = listEmployeeID1, ID = 5, Name = "HR" });
            listDepartmntEntity.Add(new DepartmentEntity() { EmployeeID = listEmployeeID2, ID = 8, Name = "Bookkeeping" });

            var department1 = new Department() { EmployeeID = listEmployeeID1, ID = 5, Name = "HR" };
            var department2 = new Department() { EmployeeID = listEmployeeID2, ID = 8, Name = "Bookkeeping" };

            listDepartment.Add(department1);
            listDepartment.Add(department2);

            //act
            A.CallTo(() => _mapper.Map<List<DepartmentEntity>, List<Department>>(listDepartmntEntity)).Returns(listDepartment);
            
            //assert
            Assert.AreEqual(listDepartmntEntity.First().EmployeeID, listDepartment.First().EmployeeID);
            Assert.AreEqual(listDepartmntEntity.First().ID, listDepartment.First().ID);
            Assert.AreEqual(listDepartmntEntity.First().Name, listDepartment.First().Name);

            Assert.AreEqual(listDepartmntEntity.Last().EmployeeID, listDepartment.Last().EmployeeID);
            Assert.AreEqual(listDepartmntEntity.Last().ID, listDepartment.Last().ID);
            Assert.AreEqual(listDepartmntEntity.Last().Name, listDepartment.Last().Name);

            Assert.AreEqual(department1, listDepartment.First());
            Assert.AreEqual(department2, listDepartment.Last());
        }
        
        [Test]
        public void Create_Department_NewDepartmentEntityInContext()
        {
            //arrange
            var department = new Department() { EmployeeID = new List<int>(new int[] {1, 2}) , ID = 5, Name = "HR" };
            var departmentEntity = new DepartmentEntity() { EmployeeID = new List<int> (new int[]{ 1, 2}), ID = 10, Name = "Development" };
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
            var department = new Department() { EmployeeID = null, ID = 15, Name = "HR" };

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
