using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using СпортКлик.Сервис;

namespace СпортКлик
{
    public class RouteConfig
    {
        static string[] пртИменПоУмолчанию = new string[] { "СпортКлик.Controllers" };
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute("Admin", "Admin/{controller}/{action}/{Ид}", new { Ид = UrlParameter.Optional }, new[] {"СпортКлик.Areas.Admin.Controllers"});
            //Категории/Беговые дорожки
            routes.MapRouteWithName("КатПроДет", "{controller}/{Имя}", new { action = Действия.ДетальноПоИмени },
                new
                {
                    controller = new ЗначениеСодержит(Контролеры.Категории, Контролеры.Производители),
                    Имя = new ЗначениеСодержит(false, Действия.Все, Действия.Добавить)
                },
                пртИменПоУмолчанию);
            //Категории/Беговые дорожки/Товары
            routes.MapRouteWithName("КатегоииТовары", "Категории/{Имя}/Товары", new { controller = "Товар", action = "ВсеДляКатегории" }, null, пртИменПоУмолчанию);

            //Производители/Kettler/Товары;
            routes.MapRouteWithName("ПроизводителиТовары", "Производители/{Имя}/Товары", new { controller = "Товар", action = "ВсеДляПроизводителя" }, null, пртИменПоУмолчанию);

            routes.MapRouteWithName("ТоварыВсеДля", "Товар/ВсеДля/{Тип}/{Имя}", new { controller = "Товар", action = "ВсеДля" }, null, пртИменПоУмолчанию);
            //Товар/Jonson 7000/34
            routes.MapRouteWithName("Товары", "Товар/{Имя}/{Ид}", new { controller = "Товар", action = "Детально" }, null, пртИменПоУмолчанию);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{Ид}",
                defaults: new { controller = "Категории", action = "Все", Ид = UrlParameter.Optional },
                namespaces: пртИменПоУмолчанию
            );
        }
    }
    public static class RouteCollectionExtensions
    {
        public static Route MapRouteWithName(this RouteCollection routes,
        string name, string url, object defaults, object constraints, string[] namespaces)
        {
            Route route = routes.MapRoute(
                name: name,
                url: url,
                defaults: defaults,
                constraints: constraints,
                namespaces: namespaces);
            if (route.DataTokens == null)
                route.DataTokens = new RouteValueDictionary();
            route.DataTokens.Add(Константы.ИмяМаршрута, name);
            return route;
        }
    }
    public class ЗначениеСодержит : IRouteConstraint
    {
        private readonly string[] _values;
        private bool _cодержит;

        public ЗначениеСодержит(params string[] values)
        {
            _cодержит = true;
            _values = values;
        }
        public ЗначениеСодержит(bool Содержит, params string[] values)
        {
            _cодержит = Содержит;
            _values = values;
        }
       
        public bool Match(HttpContextBase httpContext, Route route,
           string parameterName, RouteValueDictionary values,
           RouteDirection routeDirection)
        {
            bool сод = _values.Contains(values[parameterName].ToString(),
             StringComparer.InvariantCultureIgnoreCase);
            if (_cодержит)
                return сод;
            else
                return !сод;
        }
    }
}