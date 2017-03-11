namespace DotLiquid.Extends.Models
{
    public class BlogModel : BaseModel
    {
        public const int ALL_ARTICLE_BLOGS_ID = 0;
        public const string ALL_ARTICLE_BLOGS_ALIAS = "all";
        public const string BLOG_PAGE_BASE_URL = "/{alias}";
        private ArticlesModel _loadedArticles;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Alias { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string TemplateLayout { get; set; }

        public BlogModel()
        {

        }
        public ArticlesModel Articles
        {
            get
            {
                if (_loadedArticles == null)
                    _loadedArticles = new ArticlesModel(this);

                return _loadedArticles;
            }
        }
    }
}