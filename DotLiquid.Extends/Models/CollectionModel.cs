namespace DotLiquid.Extends.Models
{
    public class CollectionModel : BaseModel
    {
        public const int ALL_PRODUCTS_COLLECTION_ID = 0;
        public const string ALL_PRODUCTS_COLLECTION_ALIAS = "all";
        private const string COLLECTION_PAGE_BASE_URL = "/{alias}";
        private ProductsModel _loadedProduct; public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public string Type { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string SortOrder { get; set; }
        public string DefaultSortBy { get { return SortOrder; } }
        public string TemplateLayout { get; set; }

        public ProductsModel Products
        {
            get
            {
                if (_loadedProduct == null)
                    _loadedProduct = new ProductsModel(this);

                return _loadedProduct;
            }
        }
        public int ProductsCount
        {
            get
            {
                return Products.Count;
            }
        }
        public string Url
        {
            get
            {
                if (Id == ALL_PRODUCTS_COLLECTION_ID)
                    return "/collections/all";

                return COLLECTION_PAGE_BASE_URL.Replace("{alias}", Alias);
            }
        }
    }
}