using System.Collections.Generic;
using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public void Create(Department department)
        {
            _departmentRepository.Create(department);
        }

        public void Delete(int departmentId)
        {
            _departmentRepository.Delete(departmentId);
        }

        public Department Get(int employeeId)
        {
            var departmnet = _departmentRepository.Get(employeeId);

            return departmnet;
        }

        public List<Department> GetAll()
        {
            var departments = _departmentRepository.GetAll();

            return departments;
        }

        public void Update(Department department)
        {
            _departmentRepository.Update(department);
        }
    }
}
