using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Data.EF.Repositories;
using Moq;
using NUnit.Framework;
using System.Data.Entity;
using System.Linq;

namespace EmployeeManagement.Data.EF.Tests
{
    [TestFixture]
    public class DepartmentRepositoryTest
    {
        public IQueryable<EmployeeEntity> employees;
        private Mock<DbSet<EmployeeEntity>> databaseSet;
        private Mock<EmployeeContext> databaseContext;
        private DepartmentRepository repository;

        public void SetUp()
        {
            databaseSet = new Mock<DbSet<EmployeeEntity>>();
            databaseSet.As<IQueryable<EmployeeEntity>>().Setup(query => query.Provider)
                                              .Returns(employees.Provider);
            databaseSet.As<IQueryable<EmployeeEntity>>().Setup(query => query.Expression)
                                              .Returns(employees.Expression);
            databaseSet.As<IQueryable<EmployeeEntity>>().Setup(query => query.ElementType)
                                              .Returns(employees.ElementType);
            databaseSet.As<IQueryable<EmployeeEntity>>().Setup(query => query.GetEnumerator())
                                              .Returns(employees.GetEnumerator);

            databaseContext = new Mock<EmployeeContext>();
            databaseContext.Setup(context => context.Employees)
                           .Returns(databaseSet.Object);

            repository = new DepartmentRepository(databaseContext.Object);
        }

        [Test]
        public void GetAll_ReturnsAll()
        {
            //arrange
            employees = Enumerable.Repeat(new EmployeeEntity(), 2).AsQueryable();
            SetUp();

            //act
            var actual = repository.GetAll();

            //assert
            Assert.AreEqual(2, actual.Count());
        }
    }
}
