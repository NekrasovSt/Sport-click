﻿using Microsoft.Web.Mvc; //nuget:mvc4futures
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace NavigationRoutes
{
    public static class NavigationRouteConfigurationExtensions
    {

        public static void MapNavigationRoute(this RouteCollection routes, string name, string url, object defaults,
                                              object constraints = null)
        {
            var newRoute = new NamedRoute(name, url, "all", new MvcRouteHandler()) { Defaults = new RouteValueDictionary(defaults), Constraints = new RouteValueDictionary(constraints) };
            routes.Add(name, newRoute);
        }

        public static NavigationRouteBuilder MapNavigationRoute(this RouteCollection routes, string name, string displayName, string url, object defaults,
                                              object constraints = null)
        {
            var newRoute = new NamedRoute(name, displayName, url, new MvcRouteHandler()) { Defaults = new RouteValueDictionary(defaults), Constraints = new RouteValueDictionary(constraints) };
            routes.Add(name, newRoute);
            return new NavigationRouteBuilder(routes,newRoute);
        }

        public static void MapNavigationRoute(this RouteCollection routes, string name, string displayName, string url,
                                      object defaults,
                                      string[] namespaces,
                                      object constraints = null)
        {
            var newRoute = new NamedRoute(name, displayName, url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            if (namespaces != null && namespaces.Length > 0)
                newRoute.DataTokens["Namespaces"] = namespaces;

            routes.Add(name, newRoute);
        }


        public static NavigationRouteBuilder MapNavigationRoute<T>(this RouteCollection routes, string displayName, Expression<Func<T, ActionResult>> action, string areasName, string user, string icon="") where T : IController
        {
            var newRoute = new NamedRoute("", "", new MvcRouteHandler());
            newRoute.User = user;
            newRoute.ToDefaultAction(action, areasName);
            newRoute.DisplayName = displayName;
            newRoute.Icon = icon;
            routes.Add(newRoute.Name, newRoute);
            return new NavigationRouteBuilder(routes, newRoute);
        }


        public static NavigationRouteBuilder AddChildRoute<T>(this NavigationRouteBuilder builder, string DisplayText, Expression<Func<T, ActionResult>> action, string areasName, string user) where T : IController
        {
            var childRoute = new NamedRoute("", "", new MvcRouteHandler());
            childRoute.User = user;
            childRoute.ToDefaultAction<T>(action, areasName);
            childRoute.DisplayName = DisplayText;
            childRoute.IsChild = true;
            builder._parent.Children.Add(childRoute);
            builder._routes.Add(childRoute.Name,childRoute);
            return builder;
        }

        public static NamedRoute ToDefaultAction<T>(this NamedRoute route, Expression<Func<T, ActionResult>> action, string areasName) where T : IController
        {
            var body = action.Body as MethodCallExpression;

            if (body == null)
            {
                throw new ArgumentException("Expression must be a method call");
            }

            if (body.Object != action.Parameters[0])
            {
                throw new ArgumentException("Method call must target lambda argument");
            }

            string actionName = body.Method.Name;

            // check for ActionName attribute
            var attributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute)attributes[0];
                actionName = actionNameAttr.Name;
            }

            string controllerName = typeof(T).Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }

            ;
            route.Defaults = LinkBuilder.BuildParameterValuesFromExpression(body) ?? new RouteValueDictionary();
            foreach (var pair in route.Defaults.Where(x => x.Value == null).ToList())
                route.Defaults.Remove(pair.Key);

            route.Defaults.Add("controller", controllerName);
            route.Defaults.Add("action", actionName);

            route.Url = СоздатьUrl(actionName, controllerName, areasName);
            if (string.IsNullOrEmpty(areasName))
                areasName = "default";

            route.Name = String.Format("Navigation-{0}-{1}-{2}-{3}", areasName, controllerName, actionName, route.User);

            if(route.DataTokens == null)
                route.DataTokens = new RouteValueDictionary();
            route.DataTokens.Add("Namespaces", new string[] {typeof (T).Namespace});

            return route;
        }
        /// <summary>
        /// Создает УРЛ путем сложения: области, контролера, действия.
        /// </summary>
        public static string СоздатьUrl(string actionName, string controllerName, string areasName)
        {
            if (controllerName.Equals("home", StringComparison.CurrentCultureIgnoreCase))
            {
                return string.Empty;
            }
            return (string.IsNullOrEmpty(areasName)? string.Empty : areasName + "/") + controllerName.ToLower() +"/"+ actionName.ToLower();
        }

    }
    public class NavigationRouteBuilder
    {
        public NavigationRouteBuilder(RouteCollection routes, NamedRoute parent)
        {
            _routes = routes;
            _parent = parent;
        }
        public RouteCollection _routes { get; set; }
        public NamedRoute _parent { get; set; }
    }
}
