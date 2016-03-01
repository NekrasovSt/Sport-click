using Microsoft.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using СпортКлик.Сервис;

namespace NavigationRoutes
{
    public class CompositeMvcHtmlString : IHtmlString
    {
        readonly IEnumerable<IHtmlString> _strings;

        public CompositeMvcHtmlString(IEnumerable<IHtmlString> strings)
        {
            _strings = strings;
        }

        public string ToHtmlString()
        {
            return string.Join(string.Empty, _strings.Select(x => x.ToHtmlString()));
        }
    }

    public static class NavigationRoutes
    {
        public static List<INavigationRouteFilter> Filters = new List<INavigationRouteFilter>();
    }
    public static class NavigationViewExtensions
    {

        public static IHtmlString Navigation(this HtmlHelper helper)
        {
            //Складываем строку(JOIN)
            return new CompositeMvcHtmlString(
                GetRoutesForCurrentRequest(RouteTable.Routes, NavigationRoutes.Filters).Select(namedRoute => helper.NavigationListItemRouteLink(namedRoute)));
        }
        /// <summary>
        /// Формирует меню для области
        /// </summary>
        public static IHtmlString Navigation(this HtmlHelper helper, string user)
        {
            //Складываем строку(JOIN)
            return new CompositeMvcHtmlString(
                GetRoutesForCurrentRequest(RouteTable.Routes, NavigationRoutes.Filters)
                    .ПолучитьДляПользователя(user)
                    .Select(namedRoute => helper.NavigationListItemRouteLink(namedRoute)));
        }
        /// <summary>
        /// Формирует меню для роли пользователя
        /// </summary>
        public static IHtmlString Navigation(this HtmlHelper helper, System.Security.Principal.IPrincipal User)
        {
            if (User.Identity.IsAuthenticated)
                return Navigation(helper, РолиПользователей.Залогированный);
            return Navigation(helper, РолиПользователей.Все);
        }
        /// <summary>
        /// Получает колекцию NameRoute, из которой исключенны INavigationRouteFilter(не допустимые маршруты)
        /// </summary>
        public static IEnumerable<NamedRoute> GetRoutesForCurrentRequest(RouteCollection routes, IEnumerable<INavigationRouteFilter> routeFilters)
        {
            var navigationRoutes = routes.OfType<NamedRoute>().Where(r => r.IsChild == false).ToList();
            if (routeFilters.Count() > 0)
            {
                foreach (var route in navigationRoutes.ToArray())
                {
                    foreach (var filter in routeFilters)
                    {
                        if (filter.ShouldRemove(route))
                        {
                            navigationRoutes.Remove(route);
                            break;
                        }
                    }
                }
            }
            return navigationRoutes;
        }
        public static IEnumerable<NamedRoute> ПолучитьДляПользователя(this IEnumerable<NamedRoute> nameRouteCollection, string user)
        {
            return nameRouteCollection.Where(i => i.User == user);
        }

        public static MvcHtmlString NavigationListItemRouteLink(this HtmlHelper html, NamedRoute route)
        {
            var li = new TagBuilder("li");
            //Создаем ссылку с подписью.
            string routeLink = html.RouteLink(route.DisplayName, route.Name).ToString();
            li.InnerHtml = routeLink;
            //Если есть иконка добавим
            if (!string.IsNullOrEmpty(route.Icon))
            {
                li.InnerHtml = li.InnerHtml.Replace("</a>", " <i class=\"" + route.Icon + "\"></i></a>");
            }
            //Если маршрут активный выделим его (class activ)
            if (CurrentRouteMatchesName(html, route.Name))
            {
                li.AddCssClass("active");
            }
            if (route.Children.Count() > 0)
            {
                //TODO: create a UL of child routes here.
                li.AddCssClass("dropdown");
                li.InnerHtml = "<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">" + route.DisplayName + "<b class=\"caret\"></b></a>";
                var ul = new TagBuilder("ul");
                ul.AddCssClass("dropdown-menu");

                foreach (var child in route.Children)
                {
                    var childLi = new TagBuilder("li");
                    childLi.InnerHtml = html.RouteLink(child.DisplayName, child.Name).ToString();
                    ul.InnerHtml += childLi.ToString();
                }
                ul.InnerHtml += "<li>" + routeLink + "</li>";
                //that would mean we need to make some quick

                li.InnerHtml = "<a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + route.DisplayName + " <b class='caret'></b></a>" + ul.ToString();

            }
            return MvcHtmlString.Create(li.ToString(TagRenderMode.Normal));
        }
        /// <summary>
        /// Проверяет на совпадение текушего маршрута из контекста и заданного
        /// </summary>
        static bool CurrentRouteMatchesName(HtmlHelper html, string routeName)
        {
            var namedRoute = html.ViewContext.RouteData.Route as NamedRoute;
            if (namedRoute != null)
            {
                if (string.Equals(routeName, namedRoute.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }


}