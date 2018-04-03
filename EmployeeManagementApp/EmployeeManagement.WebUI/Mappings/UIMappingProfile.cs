﻿using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;
using AutoMapper;

namespace EmployeeManagement.WebUI.Mappings
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                    .ForMember(s => s.Sex, opt => opt.MapFrom(c => c.Sex))
                    .ForMember(s => s.DepartmentName, opt => opt.MapFrom(c => c.Department.Name));

            CreateMap<Department, DepartmentModel>();
            CreateMap<Developer, DeveloperModel>();
            CreateMap<ServiceWorker, ServiceWorkerModel>();
            CreateMap<Manager, ManagerModel>()
                .ForMember(s => s.Employees, opt => opt.MapFrom(c => c.EmployeeID));

            CreateMap<DepartmentModel, Department>();
            CreateMap<DeveloperModel, Developer>();
            CreateMap<ManagerModel, Manager>()
                .ForMember(s => s.EmployeeID, opt => opt.MapFrom(c => c.Employees));
        }
    }
}