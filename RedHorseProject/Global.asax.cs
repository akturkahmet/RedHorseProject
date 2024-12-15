using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.AspNet.Mvc;
using Unity;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System.Web.Http;

namespace RedHorseProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new UnityContainer();

            RegisterDependencies(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "dd/MM/yyyy";

        }
        private void RegisterDependencies(IUnityContainer container)
        {
            // Bağımlılıkları burada tanımlayın
            container.RegisterType<IGenericRepository<Agency>, GenericRepository<Agency>>();
            container.RegisterType<IGenericRepository<Admin>, GenericRepository<Admin>>();
            container.RegisterType<IGenericRepository<Reservation>, GenericRepository<Reservation>>();
            container.RegisterType<IAgencyService, AgencyManager>();
            container.RegisterType<IAgencyRepository, AgencyRepository>();
            container.RegisterType<IAdminService, AdminManager>();
            container.RegisterType<IReservationService, ReservationManager>();
        }
    }
}
