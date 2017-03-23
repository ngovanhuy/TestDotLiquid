using System.Web.Mvc;
using DotLiquid.Extends.Util;
using DotLiquid.Extends.Models;
using DotLiquid.ViewEngine;
using Services.Repository;
using System.Configuration;

namespace TestLiquid.Controllers
{
    public class BaseController : Controller
    {
        public string ViewKey { get; set; }

        public string PageTitle
        {
            get
            {
                if (ViewData["page_title"] == null)
                    return string.Empty;

                return ViewData["page_title"].ToString();
            }
            set
            {
                ViewData["page_title"] = value;
            }
        }

        public string CanonicalUrl
        {
            get
            {
                if (ViewData["canonical_url"] == null)
                    return string.Empty;

                return ViewData["canonical_url"].ToString();
            }
            set
            {
                ViewData["canonical_url"] = value;
            }
        }

        public string PageDescription
        {
            get
            {
                if (ViewData["page_description"] == null)
                    return string.Empty;

                return ViewData["page_description"].ToString();
            }
            set
            {
                ViewData["page_description"] = value;
            }
        }

        public BaseController()
        {
            ViewData["all_products"] = new ProductsModel();
            ViewData["blogs"] = new BlogsModel();
            ViewData["collections"] = new CollectionsModel();
            ViewData["linklists"] = new LinklistsModel();
            ViewData["pages"] = new PagesModel();
            ViewData["settings"] = new SettingsModel();
            ViewData["theme"] = new ThemeModel();
            ViewData[ContextKey.SITE_CONFIG] = InitSiteConfiguration();
            var contentForHeaderBuilder = new ContentForHeaderBuilder();
            ViewData["content_for_header"] = contentForHeaderBuilder.Render();
            //ViewData["request.host"] = Request.Url.Host;
        }

        private ConfigurationModel InitSiteConfiguration()
        {
            var store = new StoreService().GetStore();
            var currentThemeId = store.ActiveThemeId;

            string assetPath = UriUtility.JoinUri(
                "//bizweb.dktcdn.net/",
                MediaUtility.GenerateS3FolderFromStoreId(1),
                "themes",
                currentThemeId.ToString(),
                "assets"
            );

            string filesPath = UriUtility.JoinUri(
                "//bizweb.dktcdn.net/",
                MediaUtility.GenerateS3FolderFromStoreId(1),
                "files"
            );

            string checkoutPath = UriUtility.JoinUri(
                 "//bizweb.dktcdn.net/",
                 MediaUtility.GenerateS3FolderFromStoreId(1),
                 "checkout_stylesheet",
                 currentThemeId.ToString()
             );

            string version = string.Empty;

            var siteConfiguration = new ConfigurationModel
            {
                BaseDomain = "http://bizwebstore.dev/",
                MediaDomain = "//bizweb.dktcdn.net/",
                ThumbDomain = "//bizweb.dktcdn.net/thumb/",
                AssetsPath = assetPath,
                FilesPath = filesPath,
                CheckoutPath = checkoutPath,
                AssetsVersion = version,
                GlobalVersion = ConfigurationManager.AppSettings["GlobalVersion"] != null ? ConfigurationManager.AppSettings["GlobalVersion"] : "16052016"
            };

            return siteConfiguration;
        }
    }
}