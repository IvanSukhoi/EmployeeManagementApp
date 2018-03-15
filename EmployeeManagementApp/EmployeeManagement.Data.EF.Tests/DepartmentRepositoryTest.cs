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

namespace EmployeeManagement.Data.EF.Tests
{
    public class DepartmentRepositoryTest
    {
        private EmployeeContext _fakeContext;
        private DepartmentRepository _repository;
        private IMapper _mapper;      
        public  IQueryable<DepartmentEntity> departmentEntity;

        [SetUp]
        public void SetUp()
        {
            _fakeContext = DbContextTest.CreateDbContext<EmployeeContext>();

            departmentEntity = Enumerable.Repeat(new DepartmentEntity(), 10).AsQueryable();
            var departments = Enumerable.Repeat(new Department(), 10);

            _mapper = A.Fake<IMapper>();
            A.CallTo(() => _mapper.Map<List<DepartmentEntity>, List<Department>>(A<List<DepartmentEntity>>.Ignored))
                .ReturnsLazily(() => departments.Take(departmentEntity.ToList().Count).ToList());
            _repository = new DepartmentRepository(_fakeContext, _mapper);
        }

        [Test]
        public void GetAll_ReturnsAll()
        {
            //arrange
            _fakeContext.Departments.Insert(departmentEntity.ToArray());

            //act
            var actual = _repository.GetAll();

            //assert
            Assert.AreEqual(10, actual.Count());
        }
        
        

    }
}
