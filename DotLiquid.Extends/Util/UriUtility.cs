using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DotLiquid.Extends.Util
{
    public class UriUtility
    {
        public static string SetQueryValue(string url, string queryName, string queryValue)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            string urlWithoutQueryString = GetUrlWithoutQuery(url);
            string queryString = GetQuery(url);

            var query = HttpUtility.ParseQueryString(queryString);
            query.Set(queryName, queryValue);

            return urlWithoutQueryString + "?" + query;
        }

        public static string GetUrlWithoutQuery(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (url.Contains("?"))
                    return url.Remove(url.LastIndexOf('?'));

                return url;
            }

            return string.Empty;
        }

        private static string GetQuery(string url)
        {
            if (!string.IsNullOrEmpty(url))
                if (url.Contains("?"))
                    return url.Substring(url.LastIndexOf('?'));

            return string.Empty;
        }

        public static string RemoveQuery(string url, string queryName)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            string urlWithoutQueryString = GetUrlWithoutQuery(url);
            string queryString = GetQuery(url);

            var query = HttpUtility.ParseQueryString(queryString);
            query.Remove(queryName);

            return urlWithoutQueryString + "?" + query;
        }

        public static string GetQueryValue(Uri uri, string queryName)
        {
            if (string.IsNullOrEmpty(uri.Query))
                return null;

            var query = HttpUtility.ParseQueryString(uri.Query);
            return query.Get(queryName);
        }

        public static string RemoveStartSlash(string uri)
        {
            if (!string.IsNullOrEmpty(uri))
            {
                while (uri.StartsWith("/") || uri.StartsWith("~/"))
                {
                    if (uri.StartsWith("/"))
                        uri = uri.Substring(1);

                    if (uri.StartsWith("~/"))
                        uri = uri.Substring(2);
                }
            }

            return uri;
        }

        public static string GetDomain(string url)
        {
            var sourceUri = new Uri(url);
            return sourceUri.Host;
        }

        public static string GetSubDomain(Uri url)
        {
            string subDomain = string.Empty;

            var nodes = url.Host.Split('.');
            if (nodes.Length > 2)
            {
                if (nodes[0].Equals("www"))
                    subDomain = nodes[1];
                else
                    subDomain = nodes[0];
            }

            return subDomain;
        }

        public static string JoinUri(params string[] args)
        {
            var uriBuilder = new StringBuilder();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].EndsWith("/"))
                    args[i] = args[i].Remove(args[i].Length - 1);

                uriBuilder.Append(args[i]);

                if (i != (args.Length - 1))
                    uriBuilder.Append("/");
            }

            return uriBuilder.ToString();
        }

        public static string GetThumbnailUrl(string src, Thumbnail type)
        {
            if (string.IsNullOrEmpty(src))
                return string.Empty;
            if (src.StartsWith(@"//"))
            {
                src = "http:" + src;
            }
            if (src.StartsWith("http"))
                src = new Uri(src).PathAndQuery;
            if (type.ToString().ToLower() == "original")
            {
                return string.Format("{0}{1}", "//bizweb.dktcdn.net/", src);
            }
            return string.Format("{0}{1}", "//bizweb.dktcdn.net/thumb/" + type.ToString().ToLower(), src);
        }

        public static string GetThumbnailUrl(string src, string type)
        {
            if (string.IsNullOrEmpty(src))
                return string.Empty;

            if (src.StartsWith(@"//"))
                src = "http:" + src;

            if (src.StartsWith("http"))
                src = new Uri(src).PathAndQuery;

            type = type.ToLower();
            if (type == "original")
                return string.Format("{0}{1}", "//bizweb.dktcdn.net/", RemoveStartSlash(src));

            return string.Format("{0}{1}", "//bizweb.dktcdn.net/thumb/" + type, src);
        }

        public static string RemoveHttp(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (url.StartsWith("http://"))
                return url.Replace("http://", "//");

            if (url.StartsWith("https://"))
                return url.Replace("https://", "//");

            return url;
        }
    }

    public enum Thumbnail
    {
        Pico,
        Icon,
        Thumb,
        Small,
        Compact,
        Medium,
        Large,
        Grande
    }
}
