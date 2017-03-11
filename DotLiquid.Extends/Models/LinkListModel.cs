using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class LinklistModel : BaseModel
    {
        public LinklistModel()
        {
            Links = new List<LinkModel>();
        }
        public string Key
        {
            get { return Alias; }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public List<LinkModel> Links { get; set; }
    }
}