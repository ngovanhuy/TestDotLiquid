using DotLiquid.Extends.Util;

namespace DotLiquid.Extends.Models
{
    public class ImageModel : BaseModel
    {
        public const string NOIMAGE_URL = "http://bizweb.dktcdn.net/assets/themes_support/noimage.gif";

        private string _url;

        public string Alt { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Position { get; set; }
        public string Src
        {
            get
            {
                return UriUtility.RemoveHttp(_url);
            }
            set
            {
                _url = value;
            }
        }
        public bool AttachedToVariant { get; set; }
        public int[] VariantIds { get; set; }


        public ImageModel()
        {
        }
    }
}
