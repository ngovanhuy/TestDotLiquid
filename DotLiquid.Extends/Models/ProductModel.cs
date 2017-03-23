using System;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class ProductModel : BaseModel
    {
        private ImageModel _featuredImage;
        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Vendor { get; set; }

        public string ProductType { get; set; }

        public string TemplateLayout { get; set; }

        public string Tags { get; set; }


        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string Summary { get; set; }

        public DateTime? PublishedOn { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public ImageModel FeaturedImage
        {
            get
            {
                if (_featuredImage == null)
                {
                    return new ImageModel
                    {
                        Src = ImageModel.NOIMAGE_URL,
                        ProductId = Id
                    };
                }

                return _featuredImage;
            }
            set
            {
                _featuredImage = value;
            }
        }
        public List<ImageModel> Images { get; set; }

        public ProductModel()
        {
        }
    }
}