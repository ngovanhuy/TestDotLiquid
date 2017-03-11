using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DotLiquid;
using DotLiquid.Extends.Models;
using DotLiquid.ViewEngine;
using DotLiquid.Extends.Util;

namespace DotLiquid.Extends.Filters
{
    public class UrlFilters
    {
        /// <summary>
        /// Liquid code: {{ "meo.css" | asset_url }}
        /// </summary>
        public static string AssetUrl(Context context, string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string assetUrl = input;
            ConfigurationModel siteConfiguration = null;

            if (context[ContextKey.SITE_CONFIG] is ConfigurationModel)
                siteConfiguration = (ConfigurationModel)context[ContextKey.SITE_CONFIG];

            if (siteConfiguration != null)
            {
                if (siteConfiguration.InDesignMode && context["settings"] != null)
                {
                    var settings = context["settings"] as Dictionary<string, object>;
                    if (settings.ContainsKey(input))
                        return settings[input].ToString();
                }

                assetUrl = siteConfiguration.AssetsPath + "/" + UriUtility.RemoveStartSlash(input);

                if (!string.IsNullOrEmpty(siteConfiguration.AssetsVersion))
                    assetUrl += "?" + siteConfiguration.AssetsVersion;

                if (siteConfiguration.InDesignMode && siteConfiguration.TemplateResource.Contains(input.Replace(".scss.css", ".scss")))
                    assetUrl = GetTempAssetUrl(assetUrl, siteConfiguration.AssetsVersion);
            }

            return assetUrl.ToLower();
        }

        private static string GetTempAssetUrl(string assetUrl, string assetVersion)
        {
            string tempUrl = assetUrl.Replace("/themes/", "/theme_temp/");

            if (!string.IsNullOrEmpty(assetVersion))
                tempUrl = tempUrl.Replace(assetVersion, DateTime.Now.Ticks.ToString());
            else
                tempUrl += "?" + DateTime.Now.Ticks.ToString();

            return tempUrl;
        }

        /// <summary>
        /// Liquid code: {{ "meo.css" | media_url }}
        /// </summary>
        public static string MediaUrl(Context context, string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string mediaUrl = input;
            ConfigurationModel siteConfiguration = null;

            if (context[ContextKey.SITE_CONFIG] != null)
                siteConfiguration = (ConfigurationModel)context[ContextKey.SITE_CONFIG];

            if (siteConfiguration != null)
            {
                mediaUrl = siteConfiguration.MediaDomain + "/" + UriUtility.RemoveStartSlash(input);

                if (!string.IsNullOrEmpty(siteConfiguration.AssetsVersion))
                    mediaUrl += "?" + siteConfiguration.AssetsVersion;
            }

            return mediaUrl.ToLower();
        }

        /// <summary>
        /// Liquid code: {{ "meo.png" | file_url }}
        /// </summary>
        public static string FileUrl(Context context, string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string fileUrl = input;
            ConfigurationModel siteConfiguration = null;

            if (context[ContextKey.SITE_CONFIG] != null)
                siteConfiguration = (ConfigurationModel)context[ContextKey.SITE_CONFIG];

            if (siteConfiguration != null)
            {
                fileUrl = siteConfiguration.FilesPath + "/" + UriUtility.RemoveStartSlash(input);

                if (!string.IsNullOrEmpty(siteConfiguration.AssetsVersion))
                    fileUrl += "?" + siteConfiguration.AssetsVersion;
            }

            return fileUrl.ToLower();
        }

        /// <summary>
        /// Liquid code: {{ "meo.png" | asset_url | img_url: "pico" }}
        /// </summary>
        public static string ImgUrl(string input, string thumbType)
        {
            if (string.IsNullOrEmpty(thumbType) || string.IsNullOrEmpty(input))
                return string.Empty;

            return UriUtility.GetThumbnailUrl(input, thumbType).ToLower();
        }

        /// <summary>
        /// Liquid code: {{ "api.jquery.js" | bizweb_asset_url }}
        /// </summary>
        public static string BizwebAssetUrl(Context context, string input, string version = null)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            ConfigurationModel siteConfiguration = null;

            if (context[ContextKey.SITE_CONFIG] != null)
                siteConfiguration = (ConfigurationModel)context[ContextKey.SITE_CONFIG];

            if (siteConfiguration != null)
            {
                return StringUtility.JoinString(
                    siteConfiguration.MediaDomain,
                    "assets/themes_support/",
                    UriUtility.RemoveStartSlash(input),
                    "?",
                    !string.IsNullOrEmpty(version) ? version : siteConfiguration.GlobalVersion
                );
            }

            return input;
        }

        /// <summary>
        /// Liquid code: {{ "meo.css" | checkout_asset_url }}
        /// </summary>
        public static string CheckoutAssetUrl(Context context, string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string assetUrl = input;
            ConfigurationModel siteConfiguration = null;

            if (context[ContextKey.SITE_CONFIG] is ConfigurationModel)
                siteConfiguration = (ConfigurationModel)context[ContextKey.SITE_CONFIG];

            if (siteConfiguration != null)
            {
                if (siteConfiguration.InDesignMode && context["settings"] != null)
                {
                    var settings = context["settings"] as Dictionary<string, object>;
                    if (settings.ContainsKey(input))
                        return settings[input].ToString();
                }

                assetUrl = siteConfiguration.CheckoutPath + "/" + UriUtility.RemoveStartSlash(input);

                if (!string.IsNullOrEmpty(siteConfiguration.AssetsVersion))
                    assetUrl += "?" + siteConfiguration.AssetsVersion;

                if (siteConfiguration.InDesignMode && siteConfiguration.TemplateResource.Contains(input))
                    assetUrl = GetTempCheckoutAssetUrl(assetUrl, siteConfiguration.AssetsVersion);
            }

            return assetUrl.ToLower();
        }

        private static string GetTempCheckoutAssetUrl(string assetUrl, string assetVersion)
        {
            string tempUrl = assetUrl.Replace("/checkout_stylesheet/", "/checkout_stylesheet_temp/");

            if (!string.IsNullOrEmpty(assetVersion))
                tempUrl = tempUrl.Replace(assetVersion, DateTime.Now.Ticks.ToString());
            else
                tempUrl += "?" + DateTime.Now.Ticks.ToString();

            return tempUrl;
        }

        /// <summary>
        /// Generates a html link to the vendor of the product
        /// </summary>
        public static string LinkTo(object input, string url, string title = null)
        {
            if (string.IsNullOrEmpty(title))
                return "<a href='" + url + "'>" + input + "</a>";

            return "<a href='" + url + "' title='" + title + "'>" + input + "</a>";
        }

        /// <summary>
        /// Generates a html link to the type of the product
        /// </summary>
        public static string LinkToVendor(object input)
        {
            string vendor = string.Empty;
            if (input != null)
                vendor = input.ToString();

            string url = "/vendors?query=" + vendor;
            return BuildLink(vendor, url, vendor);
        }

        private static string BuildLink(string text, string href, string title = "")
        {
            href = Uri.EscapeUriString(href);
            href = HttpUtility.HtmlEncode(href);

            var linkBuilder = new StringBuilder();
            linkBuilder.Append("<a href='");
            linkBuilder.Append(href);

            if (!string.IsNullOrEmpty(title))
            {
                linkBuilder.Append("' title='");
                linkBuilder.Append(title);
            }

            linkBuilder.Append("'>");
            linkBuilder.Append(text);
            linkBuilder.Append("</a>");
            return linkBuilder.ToString();
        }

        /// <summary>
        /// Liquid code: {{ "Cola" | link_to_type  }}
        /// </summary>
        public static string LinkToType(string input)
        {
            string type = string.Empty;
            if (input != null)
                type = input.ToString();

            string url = "/types?query=" + input;
            return BuildLink(type, url, type);
        }

        /// <summary>
        /// Creates a link to all products in a collection that have a given tag.
        /// </summary>
        public static string LinkToTag(Context context, string input, string tag)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(tag))
                return string.Empty;

            tag = StringUtility.GetSEOAlias(tag);

            string currentUrl = string.Empty;
            if (context[ContextKey.CURRENT_URL] != null)
                currentUrl = (string)context[ContextKey.CURRENT_URL];

            currentUrl = HttpUtility.UrlDecode(currentUrl);

            string url;
            if (currentUrl.StartsWith("/blogs/"))
            {
                string alias = currentUrl.Split('/').Length > 2 ? currentUrl.Split('/')[2] : string.Empty;
                url = "/blogs/" + alias + "/tagged/" + tag;
            }
            else if (currentUrl.StartsWith("/collections/"))
            {
                string alias = currentUrl.Split('/').Length > 2 ? currentUrl.Split('/')[2] : string.Empty;
                url = "/collections/" + alias + "/" + tag;
            }
            else if (currentUrl.StartsWith("/collections/vendors") || 
                     currentUrl.StartsWith("/collections/types") || 
                     currentUrl.StartsWith("/vendors") || 
                     currentUrl.StartsWith("/types"))
            {
                if (currentUrl.Contains("?"))
                    url = currentUrl + "&constrain=" + tag;
                else
                    url = currentUrl + "?constrain=" + tag;
            }
            else
            {
                string alias = currentUrl.Split('/').Length >= 2 ? currentUrl.Split('/')[1] : string.Empty;
                url = "/" + alias + "/" + tag;
            }

            return BuildLink(input, url, tag);
        }

        /// <summary>
        /// Creates a link to all products in a collection that have a given tag as well as any tags 
        /// that have been already selected.
        /// </summary>
        public static string LinkToAddTag(Context context, string input, string tag)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(tag))
                return string.Empty;

            tag = StringUtility.GetSEOAlias(tag);

            string currentUrl = string.Empty;
            if (context[ContextKey.CURRENT_URL] != null)
                currentUrl = (string)context[ContextKey.CURRENT_URL];

            string url;
            if (currentUrl.StartsWith("/blogs/"))
            {
                if (!currentUrl.Contains("/tagged/"))
                    return LinkToTag(context, input, tag);

                var tagsString = currentUrl.Substring(currentUrl.IndexOf("/tagged/") + "/tagged/".Length);
                var tags = new List<string>(HttpUtility.UrlDecode(tagsString).ToLower().Split('+'));
                if (!tags.Contains(tag))
                    tags.Add(tag);

                tags.Remove(string.Empty);

                string baseUrl = currentUrl.Replace(tagsString, "");
                url = AppendTag(baseUrl, tags);
            }
            else if (currentUrl.StartsWith("/collections/"))
            {
                var productRegex = new Regex(@"(\/collections\/)(\S+)\/products");
                var match = productRegex.Match(currentUrl);
                if (match.Success)
                    return LinkToTag(context, input, tag);

                var parts = currentUrl.Split('/');
                if (parts.Length < 4)
                    return LinkToTag(context, input, tag);

                string tagsString = parts[3];
                var tags = new List<string>(tagsString.ToLower().Split('+'));
                if (!tags.Contains(tag))
                    tags.Add(tag);

                tags.Remove(string.Empty);

                string baseUrl = string.Empty;
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    baseUrl += parts[i];
                    baseUrl += "/";
                }

                url = AppendTag(baseUrl, tags);
            }
            else
            {
                var parts = currentUrl.Split('/');
                if (parts.Length < 3)
                    return LinkToTag(context, input, tag);

                string tagsString = parts[2];
                var tags = new List<string>(tagsString.ToLower().Split('+'));
                if (!tags.Contains(tag))
                    tags.Add(tag);

                tags.Remove(string.Empty);

                string baseUrl = string.Empty;
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    baseUrl += parts[i];
                    baseUrl += "/";
                }

                url = AppendTag(baseUrl, tags);
            }

            return BuildLink(input, url, tag);
        }

        /// <summary>
        /// Generates a link to all products in a collection that have the given tag and 
        /// all the previous tags that might have been added already.
        /// </summary>
        public static string LinkToRemoveTag(Context context, string input, string tag)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(tag))
                return string.Empty;

            tag = StringUtility.GetSEOAlias(tag);

            string currentUrl = string.Empty;
            if (context[ContextKey.CURRENT_URL] != null)
                currentUrl = (string)context[ContextKey.CURRENT_URL];

            string url;
            if (currentUrl.StartsWith("/blogs/"))
            {
                if (!currentUrl.Contains("/tagged/"))
                    return LinkToTag(context, input, tag);

                var tagsString = currentUrl.Substring(currentUrl.IndexOf("/tagged/") + "/tagged/".Length);
                var tags = new List<string>(HttpUtility.UrlDecode(tagsString).ToLower().Split('+'));
                if (tags.Contains(tag))
                    tags.Remove(tag);

                tags.Remove(string.Empty);

                string baseUrl = currentUrl.Replace(tagsString, "");
                url = tags.Count > 0 ? AppendTag(baseUrl, tags) : baseUrl.Replace("/tagged/", "");
            }
            else if (currentUrl.StartsWith("/collections/"))
            {
                var productRegex = new Regex(@"(\/collections\/)(\S+)\/products");
                var match = productRegex.Match(currentUrl);
                if (match.Success)
                    return LinkToTag(context, input, string.Empty);

                var parts = currentUrl.Split('/');
                if (parts.Length < 4)
                    return LinkToTag(context, input, tag);

                string tagsString = parts[3];
                var tags = new List<string>(tagsString.ToLower().Split('+'));
                if (tags.Contains(tag))
                    tags.Remove(tag);

                tags.Remove(string.Empty);

                string baseUrl = string.Empty;
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    baseUrl += parts[i];
                    baseUrl += "/";
                }

                url = AppendTag(baseUrl, tags);
            }
            else
            {
                var parts = currentUrl.Split('/');
                if (parts.Length < 3)
                    return LinkToTag(context, input, tag);

                string tagsString = parts[2];
                var tags = new List<string>(tagsString.ToLower().Split('+'));
                if (tags.Contains(tag))
                    tags.Remove(tag);

                tags.Remove(string.Empty);

                string baseUrl = string.Empty;
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    baseUrl += parts[i];
                    baseUrl += "/";
                }

                url = AppendTag(baseUrl, tags);
            }

            return BuildLink(input, url, tag);
        }

        private static string AppendTag(string url, List<string> tags)
        {
            if (tags.Count == 0)
                return url;

            var urlBuilder = new StringBuilder();
            urlBuilder.Append(url);
            urlBuilder.Append(tags[0]);

            for (int i = 1; i < tags.Count; i++)
            {
                urlBuilder.Append("+");
                urlBuilder.Append(tags[i]);
            }

            return urlBuilder.ToString();
        }

        /// <summary>
        /// Creates a URL that links to a collection page containing products with a specific product vendor.
        /// </summary>
        public static string UrlForVendor(object input)
        {
            string vendor = string.Empty;
            if (input != null)
                vendor = input.ToString();

            return "/vendors?query=" + Uri.EscapeUriString(vendor);
        }

        /// <summary>
        /// Creates a URL that links to a collection page containing products with a specific product type.
        /// </summary>
        public static string UrlForType(string input)
        {
            string type = string.Empty;
            if (input != null)
                type = input.ToString();

            return "/types?query=" + Uri.EscapeUriString(type);
        }

        /// <summary>
        /// Liquid code: {{ "Edit Address" | edit_customer_address_link: 3 }}
        /// </summary>
        public static string EditCustomerAddressLink(string input, int id)
        {
            return "<a href='javascript:;' onclick='Bizweb.CustomerAddress.toggleEditForm(" + id + ");return false'>" + input + "</a>";
        }

        /// <summary>
        /// Liquid code: {{ "Delete Address" | delete_customer_address_link: 3, "Xóa sổ địa chỉ thành công" }}
        /// </summary>
        public static string DeleteCustomerAddressLink(string input, int id, string message = null)
        {
            if (string.IsNullOrEmpty(message))
                return "<a href='javascript:;' onclick='Bizweb.CustomerAddress.destroy(" + id + ");return false'>" + input + "</a>";

            return "<a href='javascript:;' onclick='Bizweb.CustomerAddress.destroy(" + id + ", '" + message + "');return false'>" + input + "</a>";
        }

        /// <summary>
        /// Liquid code: {{ 'Log in' | customer_login_link }}
        /// </summary>
        public static string CustomerLoginLink(object input)
        {
            return "<a href='/account/login' id='customer_login_link'>" + input + "</a>";
        }

        /// <summary>
        /// Liquid code: {{ 'Log in' | customer_logout_link }}
        /// </summary>
        public static string CustomerLogoutLink(object input)
        {
            return "<a href='/account/logout' id='customer_logout_link'>" + input + "</a>";
        }

        /// <summary>
        /// Liquid code: {{ 'Log in' | customer_register_link }}
        /// </summary>
        public static string CustomerRegisterLink(object input)
        {
            return "<a href='/account/register' id='customer_register_link'>" + input + "</a>";
        }

        /// <summary>
        /// Creates a collection-aware product URL by prepending "/collections/collection-alias/" to a product URL, 
        /// where "collection-handle" is the handle of the collection that is currently being viewed.
        /// </summary>
        public static string Within(string input, object collection)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            if (collection != null && collection.GetType().Name == "CollectionModel")
            {
                object collectionUrl = collection.GetType().GetProperty("Url").GetValue(collection, null);
                if (collectionUrl != null && !string.IsNullOrEmpty(collectionUrl.ToString()))
                    input = CleanWithinCollectionUrl(collectionUrl.ToString()) + CleanWithinInput(input);
            }

            return input;
        }

        private static string CleanWithinCollectionUrl(string collectionUrl)
        {
            if (!collectionUrl.Contains("/collections/"))
                collectionUrl = "/collections" + collectionUrl;

            return collectionUrl;
        }

        private static string CleanWithinInput(string input)
        {
            if (!input.StartsWith("/"))
                input = "/" + input;

            if (!input.Contains("/products/"))
                input = "/products" + input;

            return input;
        }
    }
}
