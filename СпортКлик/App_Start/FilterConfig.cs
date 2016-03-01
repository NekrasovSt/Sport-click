using System.Web;
using System.Web.Mvc;
using СпортКлик.Фильтры;

namespace СпортКлик
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ФильтрИсключений());
        }
    }
}