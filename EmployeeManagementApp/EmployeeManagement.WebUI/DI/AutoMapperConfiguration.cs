using AutoMapper;
using EmployeeManagement.Data.EF.Mapp;
using EmployeeManagement.WebUI.Mappings;

namespace EmployeeManagement.WebUI.DI
{
    public static class AutoMapperConfiguration 
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DataMappingProfile());
                cfg.AddProfile(new UIMappingProfile());
            });
        }
    }
}