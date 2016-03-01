using System.Web.Mvc;

namespace СпортКлик.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{Ид}",
                new { action = "Index", Ид = UrlParameter.Optional }, new string[] { "СпортКлик.Areas.Admin.Controllers" }
            );
        }
    }
}
