using System.Web;
using System.Web.Optimization;

namespace Projeto_Academico
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/startPage/js/jquery.min.js",
                        "~/Scripts/startPage/js/bootstrap.min.js",
                        "~/Scripts/startPage/js/creative.min.js",
                        "~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/bootstrap.min.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/startPage/js/scrollreveal.min.js",
                      "~/Scripts/startPage/js/jquery.magnific-popup.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/cadastroInicial").Include(
                      "~/Scripts/cadastroInicial/js/scripts.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/adminTheme", "https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js").Include(
                      "~/Scripts/adminTheme/jquery-{version}.js",
                      "~/Scripts/adminTheme/tether.min.js",
                      "~/Scripts/adminTheme/bootstrap.min.js",
                      "~/Scripts/adminTheme/jquery.cookie.js",
                      "~/Scripts/adminTheme/jquery.validate.min.js",
                      "~/Scripts/adminTheme/charts-home.js",
                      "~/Scripts/adminTheme/front.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/adminTheme/js", "https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js").Include(
                    "~/Scripts/adminTheme/charts-home.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/ajax/js").Include(
                    "~/Scripts/jquery.unobtrusive-ajax.js",
                    "~/Scripts/jquery.unobtrusive-ajax.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/startPage").Include(
                      "~/Content/startPage/css/bootstrap.min.css",
                      "~/Content/startPage/css/font-awesome.min.css",
                      "~/Content/startPage/css/magnific-popup.css",
                      "~/Content/startPage/css/creative.min.css",
                      "~/Content/startPage/css/style.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/cadastro").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/cadastro/assets/css/style.css"
                    ));

            bundles.Add(new StyleBundle("~/Content/adminTheme").Include(
                    "~/Content/adminTheme/bootstrap.css",
                    "~/Content/adminTheme/bootstrap.min.css",
                    "~/Content/adminTheme/font-awesome.min.css",
                    "~/Content/adminTheme/style.default.css",
                    "~/Content/adminTheme/custom.css"
                ));

            bundles.Add(new StyleBundle("~/Content/kanban").Include(
                    "~/Content/kanban/bootstrap.min.css",
                    "~/Content/adminTheme/font-awesome.min.css",
                    "~/Content/kanban/styleKanban.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/kanban").Include(
                    "~/Content/kanban/kanban-style.js"
                ));



            //bundles.Add(new ScriptBundle("~/bundles/kanban").Include(
            //       "~/Content/kanban/angular-route.min.js",
            //       "~/Content/kanban/angular-sanitize.min.js",
            //       "~/Content/kanban/angular-spectrum-colorpicker.min.js",
            //       "~/Content/kanban/angular.min.min.js",
            //       "~/Content/kanban/bootstrap.min.js",
            //       "~/Content/kanban/jquery-ui.min.js",
            //       "~/Content/kanban/jquery.min.js",
            //       "~/Content/kanban/scripts.9bb3f574.js",
            //       "~/Content/kanban/spectrum.js",
            //       "~/Content/kanban/spin.js",
            //       "~/Content/kanban/themes.82ae94b8.js"
            //    ));
        }
    }
}
