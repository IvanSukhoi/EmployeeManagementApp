using AutoMapper;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Data.EF.Mapp
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<DepartmentEntity, Department>()
                .ForMember(s => s.CatalogEmployeeID, opt => opt.MapFrom(c => c.CatalogEmployeeID));

            CreateMap<Department, DepartmentEntity>()
                .ForMember(s => s.CatalogEmployeeID, opt => opt.MapFrom(c => c.CatalogEmployeeID));

            CreateMap<EmployeeEntity, Developer>()
                    .ForMember(x => x.Tasks, opt => opt.Ignore())
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department));

            CreateMap<EmployeeEntity, Manager>()
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                    .ForMember(s => s.EmployeeID, opt => opt.Ignore());

            CreateMap<EmployeeEntity, ServiceWorker>()
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                    .ForMember(s => s.TypeOfWorker, opt => opt.MapFrom(c => c.Profession));

            CreateMap<Developer, EmployeeEntity>()
                    .ForMember(s => s.Profession, opt => opt.MapFrom(c => Profession.Developer))
                    .ForMember(s => s.Position, opt => opt.MapFrom(c => c.Position))
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department));

            CreateMap<Manager, EmployeeEntity>()
                    .ForMember(s => s.Profession, opt => opt.MapFrom(c => Profession.Manager))
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department));

            CreateMap<ServiceWorker, EmployeeEntity>()
                    .ForMember(s => s.Profession, opt => opt.MapFrom(c => c.TypeOfWorker))
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department));

        }
    }
}
