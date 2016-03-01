using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using СпортКлик.Controllers;

namespace СпортКлик.Фильтры
{
    public class ФильтрИсключений : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {

            //http://habrahabr.ru/post/137672/
            var statusCode = (int)HttpStatusCode.InternalServerError;
            if (filterContext.Exception is HttpException)
            {
                statusCode = (filterContext.Exception as HttpException).GetHttpCode();
            }
            else if (filterContext.Exception is UnauthorizedAccessException)
            {
                //to prevent login prompt in IIS
                // which will appear when returning 401.
                statusCode = (int)HttpStatusCode.Forbidden;
            }
            //_logger.Error("Uncaught exception", filterContext.Exception);

            var result = CreateActionResult(filterContext, statusCode);
            filterContext.Result = result;

            //Получает или задает значение, указывающее, выполнена ли обработка исключения.
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        protected virtual ActionResult CreateActionResult(ExceptionContext filterContext, int statusCode)
        {
            var ctx = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
            var statusCodeName = ((HttpStatusCode)statusCode).ToString();

            var viewName = SelectFirstView(ctx,
                                           string.Format("~/Views/Error/{0}.cshtml", statusCodeName),
                                           "~/Views/Error/General.cshtml",
                                           statusCodeName,
                                           "Error");

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            string message = filterContext.RouteData.Values.Aggregate(
                                        new StringBuilder(), (sb, kv) =>
                                            sb.AppendFormat(
                                                "({0} = {1}),",
                                                kv.Key,
                                                kv.Value
                                            )
                                    ).ToString();
            Logger log = LogManager.GetLogger("Logger");
            log.LogException(LogLevel.Trace, message, filterContext.Exception);
            //Инкапсулирует сведения для обработки ошибки, вызванной методом действия. 
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            var result = new ViewResult
                             {
                                 ViewName = viewName,
                                 ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                             };
            result.ViewBag.StatusCode = statusCode;
            return result;
        }
        /// <summary>
        /// Находит представление для контролера
        /// </summary>
        protected string SelectFirstView(ControllerContext ctx, params string[] viewNames)
        {
            return viewNames.First(view => ViewExists(ctx, view));
        }
        protected bool ViewExists(ControllerContext ctx, string name)
        {
            ViewEngineResult result = ViewEngines.Engines.FindView(ctx, name, null);
            return result.View != null;
        }
    }
}