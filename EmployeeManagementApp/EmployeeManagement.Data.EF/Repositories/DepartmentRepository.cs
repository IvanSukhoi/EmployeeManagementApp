using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Data.EF.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeContext _dbContext;
        private readonly IMapper _mapper;

        public DepartmentRepository(EmployeeContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Create(Department department)
        {
            var departmentEntity = _mapper.Map<Department, DepartmentEntity>(department);   
           _dbContext.Departments.Add(departmentEntity);
        }

        public List<Department> GetAll()
        {
            var departments = _mapper.Map<List<DepartmentEntity>, List<Department>>(_dbContext.Departments.ToList());
            return departments;
        }

        public void Delete(int departmentId)
        {
            DepartmentEntity departmentEntity = _dbContext.Departments.FirstOrDefault(x => x.ID == departmentId);
            if (departmentEntity != null)
                _dbContext.Departments.Remove(departmentEntity);
        }

        public Department Get(int departmentId)
        {
            var department = _mapper.Map<DepartmentEntity, Department>(_dbContext.Departments.FirstOrDefault(x => x.ID == departmentId));
            return department;
        }

        public void Update(Department department)
        {

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
