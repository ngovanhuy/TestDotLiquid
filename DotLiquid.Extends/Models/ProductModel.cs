using System;

namespace DotLiquid.Extends.Models
{
    public class ProductModel : BaseModel
    {
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

        public ProductModel()
        {
        }
    }
}