﻿using EmployeeManagement.Domain.Models;
using System.Collections.Generic;

namespace EmployeeManagement.Domain.DataInterfaces
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        Department Get(int departmentId);
        void Create(Department department);
        void Update(Department department);
        void Delete(int departmentId);
        void Save();
    }
}
