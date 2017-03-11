namespace Services.Filter
{
    public class CollectionFilter : PaginateFilter
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool? Published { get; set; }
        public string CollectionType { get; set; }
    }
}
