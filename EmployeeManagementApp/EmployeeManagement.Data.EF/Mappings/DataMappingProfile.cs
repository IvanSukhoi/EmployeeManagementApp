using AutoMapper;
using EmployeeManagement.Data.EF.Entities;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Data.EF.Mappings
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<DepartmentEntity, Department>();

            CreateMap<Department, DepartmentEntity>();

            CreateMap<EmployeeEntity, Developer>()
                    .ForMember(x => x.Tasks, opt => opt.Ignore())
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                    .ForMember(s => s.MiddleName, opt => opt.MapFrom(c => c.MidleName));

            CreateMap<EmployeeEntity, Manager>()
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                    .ForMember(s => s.EmployeeID, opt => opt.Ignore())
                    .ForMember(s => s.MiddleName, opt => opt.MapFrom(c => c.MidleName));

            CreateMap<EmployeeEntity, ServiceWorker>()
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                    .ForMember(s => s.TypeOfWorker, opt => opt.MapFrom(c => c.Profession))
                    .ForMember(s => s.MiddleName, opt => opt.MapFrom(c => c.MidleName));

            CreateMap<Developer, EmployeeEntity>()
                    .ForMember(s => s.Profession, opt => opt.MapFrom(c => Profession.Developer))
                    .ForMember(s => s.Position, opt => opt.MapFrom(c => c.Position))
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                    .ForMember(s => s.MidleName, opt => opt.MapFrom(c => c.MiddleName)); ;

            CreateMap<Manager, EmployeeEntity>()
                    .ForMember(s => s.Profession, opt => opt.MapFrom(c => Profession.Manager))
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                    .ForMember(s => s.MidleName, opt => opt.MapFrom(c => c.MiddleName)); ;

            CreateMap<ServiceWorker, EmployeeEntity>()
                    .ForMember(s => s.Profession, opt => opt.MapFrom(c => c.TypeOfWorker))
                    .ForMember(s => s.Department, opt => opt.MapFrom(c => c.Department))
                    .ForMember(s => s.MidleName, opt => opt.MapFrom(c => c.MiddleName)); ;
        }
    }
}
