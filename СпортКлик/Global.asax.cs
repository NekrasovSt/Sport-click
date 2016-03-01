using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using СпортКлик.Сервис;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Controllers;
using NLog;
using System.Web.Security;
using СпортКлик.Models;
using СпортКлик.Binders;
namespace СпортКлик
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Регистрация областей
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Регистрация контента
            BootstrapSupport.BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Регистрация маршрутв для меню
            BootstrapMvcSample.ExampleLayoutsRouteConfig.RegisterRoutes(RouteTable.Routes);
            //Регистрация фабрики контролеров
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
            //Связывание модели корзины
            ModelBinders.Binders.Add(typeof(Корзина), new КорзинаПривязкаМодели());
            ModelBinders.Binders.Add(typeof(Пользователь), new ПользовательПривязкаМодели());
        }
        public static void AddAdmin()
        {
            // Если нет в системе роли admin, создаём её
            if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");

            // Если нет в системе пользователя admin, создаём его
            if (Membership.GetUser("admin") == null)
            {
                MembershipCreateStatus status = MembershipCreateStatus.Success;
                Membership.CreateUser("admin", "temp_pass", "info@21century.com", "My favourite writer", "Jules Verne", true, out status);
            }

            // Если у пользователя admin нет роли admin, присваиваем ему эту роль
            if (!Roles.IsUserInRole("admin", "Admin"))
                Roles.AddUserToRole("admin", "Admin");
        }

    }
}