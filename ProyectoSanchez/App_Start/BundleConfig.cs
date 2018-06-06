using System.Web;
using System.Web.Optimization;

namespace ProyectoSanchez
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/layoutcss")
                .Include(
                "~/Content/vendors/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                "~/Content/vendors/animate.css/animate.min.css",
                "~/Content/vendors/iCheck/skins/flat/green.css",
                "~/Content/vendors/nprogress/nprogress.css",
                "~/Content/build/css/custom.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/layoutjs")
                .Include(

                "~/Content/vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/vendors/fastclick/lib/fastclick.js",
                "~/Content/vendors/iCheck/icheck.min.js",
                "~/Content/vendors/nprogress/nprogress.js",
                "~/Content/build/js/custom.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                "~/Content/vendors/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/loginjs")
                .Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/additional-methods.min.js",
                "~/Content/build/js/login.js"));
        }
    }
}
