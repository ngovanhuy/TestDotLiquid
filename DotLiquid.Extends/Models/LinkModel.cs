using DotLiquid.Extends.Util;

namespace DotLiquid.Extends.Models
{
    public class LinkModel : BaseModel
    {
        public const string COLLECTION_LINK_TYPE = "collection";
        public const string PRODUCT_LINK_TYPE = "product";
        public const string PAGE_LINK_TYPE = "page";
        public const string BLOG_LINK_TYPE = "blog";
        public const string FRONTPAGE_LINK_TYPE = "frontpage";
        public const string ALL_PRODUCT_LINK_TYPE = "catalog";
        public const string SEARCH_LINK_TYPE = "search";
        public const string HTTP_LINK_TYPE = "http";
        private string _loadedUrl;
        private string _loadedAlias;

        public string Title { get; set; }

        public string BaseType { get; set; }

        public string Tags { get; set; }

        public string BaseUrl { get; set; }

        public string Type
        {
            get
            {
                switch (BaseType.ToLower())
                {
                    case COLLECTION_LINK_TYPE:
                        return "collection_link";
                    case PRODUCT_LINK_TYPE:
                        return "product_link";
                    case PAGE_LINK_TYPE:
                        return "page_link";
                    case BLOG_LINK_TYPE:
                        return "blog_link";
                    case FRONTPAGE_LINK_TYPE:
                    case ALL_PRODUCT_LINK_TYPE:
                    case SEARCH_LINK_TYPE:
                        return "relative_link";
                    case HTTP_LINK_TYPE:
                        return "http_link";
                    default:
                        return string.Empty;
                }
            }
        }

        public string Alias
        {
            get
            {
                if (_loadedAlias == null)
                    _loadedAlias = StringUtility.GetSEOAlias(Title);

                return _loadedAlias;
            }
        }
        public string Url
        {
            get
            {
                if (_loadedUrl == null)
                {
                    switch (BaseType.ToLower())
                    {
                        case COLLECTION_LINK_TYPE:
                            if (string.IsNullOrEmpty(Tags))
                                _loadedUrl = "/" + BaseUrl;
                            else
                                _loadedUrl = "/" + BaseUrl + "/" + Tags;

                            break;
                        case PRODUCT_LINK_TYPE:
                            _loadedUrl = "/" + BaseUrl;
                            break;
                        case PAGE_LINK_TYPE:
                            _loadedUrl = "/" + BaseUrl;
                            break;
                        case BLOG_LINK_TYPE:
                            _loadedUrl = "/" + BaseUrl;
                            break;
                        case FRONTPAGE_LINK_TYPE:
                            _loadedUrl = "/";
                            break;
                        case ALL_PRODUCT_LINK_TYPE:
                            _loadedUrl = "/collections/all";
                            break;
                        case SEARCH_LINK_TYPE:
                            _loadedUrl = "/search";
                            break;
                        default:
                            _loadedUrl = BaseUrl;
                            break;
                    }
                }

                return _loadedUrl;
            }
        }

    }
}