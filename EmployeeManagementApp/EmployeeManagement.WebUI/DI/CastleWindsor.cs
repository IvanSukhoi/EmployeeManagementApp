using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using EmployeeManagement.Data.EF.DAL;
using EmployeeManagement.Data.EF.Repositories;
using EmployeeManagement.Domain.DataInterfaces;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.DomainServices;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.DI
{
    public class CastleWindsor : IWindsorInstaller
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static void Dispose()
        {
            _container.Dispose();
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMapper>().ImplementedBy<Mapper>().LifeStyle.PerWebRequest);
            container.Register(Component.For<EmployeeContext>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IEmployeeRepository>().ImplementedBy<EmployeeRepository>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IDepartmentRepository>().ImplementedBy<DepartmentRepository>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IManagerService>().ImplementedBy<ManagerService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IEmployeeService>().ImplementedBy<EmployeeService>().LifeStyle.PerWebRequest);

            container.Register(AllTypes.FromThisAssembly()
                    .BasedOn<IController>()
                    .If(t => t.Name.EndsWith("Controller")).LifestylePerWebRequest());
        }
    }
}
