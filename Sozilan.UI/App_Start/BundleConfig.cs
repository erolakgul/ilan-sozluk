using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Sozilan.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            #region Users

            bundles.Add(new StyleBundle("~/bundles/admin/styles").Include(
                "~/Areas/Users/Content/css/bootstrap.css",
                "~/Areas/Users/Content/css/bootstrap.social.css",
                "~/Areas/Users/Content/css/sweetalert.css",
                "~/Areas/Users/Content/css/default.css").Include("~/Areas/Users/Content/css/font-awesome.min.css", new CssRewriteUrlTransform()
                ));
            bundles.Add(new ScriptBundle("~/bundles/admin/scripts").Include(
                "~/Areas/Users/Scripts/js/jquery-1.10.2.min.js",
                "~/Areas/Users/Scripts/js/bootstrap.min.js",
                "~/Areas/Users/Scripts/js/sweetalert-dev.js",
                "~/Areas/Users/Scripts/js/DefaultAdmin.js",
                "~/Areas/Users/Scripts/js/SearchScript.js"
                ));
            // salt okunur kısım için

            bundles.Add(new StyleBundle("~/bundles/content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap.social.css",
                "~/Content/sweetalert.css",
                "~/Content/defaultSite.css").Include("~/Content/font-awesome.css", new CssRewriteUrlTransform()
                ));
            bundles.Add(new ScriptBundle("~/bundles/script/js").Include(
                "~/Scripts/jquery-1.10.2.js",
                "~/Scripts/sweetalert-dev.js",
                "~/Scripts/modernizr-2.6.2.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/Default.js",
                "~/Scripts/SearchScript.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js"
                ));
            #endregion
        }
    }
}