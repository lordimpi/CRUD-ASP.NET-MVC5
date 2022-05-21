[assembly: WebActivator.PostApplicationStartMethod(typeof(CRUD_MVC_5.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace CRUD_MVC_5.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using CRUD_MVC_5.Data;
    using CRUD_MVC_5.Repositories;
    using CRUD_MVC_5.Repositories.Contracts;
    using CRUD_MVC_5.Service;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            container.Register<IPersonaRepository, PersonaRepository>(Lifestyle.Scoped);
            container.Register<IPersonaService, PersonaService>(Lifestyle.Scoped);
            container.Register<DataAccess>(Lifestyle.Scoped);
        }
    }
}