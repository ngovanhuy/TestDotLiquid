namespace DotLiquid.Extends.Models
{
    public class ArticleModel : BaseModel
    {
        public const string ARTICLE_PAGE_BASE_URL = "/{alias}";
        public string Key
        {
            get { return Alias; }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public int? UserId { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string Summary { get; set; }

        public string TemplateLayout { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }


        public string Url
        {
            get
            {
                return ARTICLE_PAGE_BASE_URL.Replace("{alias}", Alias);
            }
        }
    }
}