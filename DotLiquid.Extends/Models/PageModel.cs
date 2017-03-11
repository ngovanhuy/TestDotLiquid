namespace DotLiquid.Extends.Models
{
    public class PageModel : BaseModel
    {
        private const string PAGE_PAGE_BASE_URL = "/{alias}";
        public  string Key
        {
            get { return Alias; }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Author { get; set; }
        public string TemplateLayout { get; set; }
        public string Content { get; set; }

        public string Url
        {
            get
            {
                return PAGE_PAGE_BASE_URL.Replace("{alias}", Alias);
            }
        }
    }
}