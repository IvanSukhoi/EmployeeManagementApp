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

            CreateMap<Developer, DeveloperModel>()
                .ForMember(s => s.DepartmentName, opt => opt.MapFrom(c => c.Department.Name))
                .ForMember(s => s.ModelType, opt => opt.MapFrom(c => ModelType.Developer));

            CreateMap<ServiceWorker, ServiceWorkerModel>()
                .ForMember(s => s.DepartmentName, opt => opt.MapFrom(c => c.Department.Name))
                .ForMember(s => s.ModelType, opt => opt.MapFrom(c => ModelType.ServiceWorker));

            CreateMap<Manager, ManagerModel>()
                .ForMember(s => s.DepartmentName, opt => opt.MapFrom(c => c.Department.Name))
                .ForMember(s => s.Employees, opt => opt.MapFrom(c => c.EmployeeID))
                .ForMember(s => s.ModelType, opt => opt.MapFrom(c => ModelType.Manager));


            CreateMap<DeveloperModel, Developer>();

            CreateMap<ManagerModel, Manager>()
                .ForMember(s => s.EmployeeID, opt => opt.MapFrom(c => c.Employees));

            CreateMap<ServiceWorkerModel, ServiceWorker>();
        }
    }
}