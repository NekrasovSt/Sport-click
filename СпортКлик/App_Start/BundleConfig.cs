using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace BootstrapSupport
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"
                ));
            //WYSI редактор 
            bundles.Add(new ScriptBundle("~/ckeditor").Include(
                "~/Scripts/ckeditor/ckeditor.js"));
            //Для регистрации
            bundles.Add(new ScriptBundle("~/common").Include("~/Scripts/Common.js"));
            //ColorBox
            bundles.Add(new ScriptBundle("~/colorbox").Include("~/Scripts/jquery.colorbox.js"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/bootstrap.css", "~/Content/Custom.css"
                ));
            bundles.Add(new StyleBundle("~/content/css-responsive").Include(
                "~/Content/bootstrap-responsive.css"
                ));
            //ColorBox
            bundles.Add(new StyleBundle("~/content/colorbox").Include("~/content/colorbox.css"));
        }
    }
}