namespace DotLiquid.Extends.Models
{
    public class PartModel : BaseModel
    {
        public bool IsLink { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
