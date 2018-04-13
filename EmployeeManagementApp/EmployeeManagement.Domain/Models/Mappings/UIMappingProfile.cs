using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;
using AutoMapper;

namespace EmployeeManagement.WebUI.Mappings
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap<Department, DepartmentModel>();
            CreateMap<DepartmentModel, Department>();

            CreateMap<Employee, EmployeeModel>()
                .ForMember(s => s.Sex, opt => opt.MapFrom(c => c.Sex))
                .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department));

            CreateMap<Developer, DeveloperModel>()
                .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department));

            CreateMap<ServiceWorker, ServiceWorkerModel>()
                .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department));

            CreateMap<Manager, ManagerModel>()
                .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                .ForMember(s => s.Employees, opt => opt.MapFrom(c => c.EmployeeID));


            CreateMap<DeveloperModel, Developer>();

            CreateMap<ManagerModel, Manager>()
                .ForMember(s => s.EmployeeID, opt => opt.MapFrom(c => c.Employees));

            CreateMap<ServiceWorkerModel, ServiceWorker>();
        }
    }
}