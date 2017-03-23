using Jil;
using System;
using System.Collections.Generic;

namespace Services.Domain
{
    public class Product
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "name")]
        public string Name { get; set; }

        [JilDirective(Name = "alias")]
        public string Alias { get; set; }

        [JilDirective(Name = "vendor")]
        public string Vendor { get; set; }

        [JilDirective(Name = "product_type")]
        public string ProductType { get; set; }

        [JilDirective(Name = "template_layout")]
        public string TemplateLayout { get; set; }

        [JilDirective(Name = "tags")]
        public string Tags { get; set; }

        [JilDirective(Name = "meta_title")]
        public string MetaTitle { get; set; }

        [JilDirective(Name = "meta_description")]
        public string MetaDescription { get; set; }

        [JilDirective(Name = "summary")]
        public string Summary { get; set; }

        [JilDirective(Name = "published_on")]
        public DateTime? PublishedOn { get; set; }

        [JilDirective(Name = "content")]
        public string Content { get; set; }

        [JilDirective(Name = "image")]
        public ProductImage Image { get; set; }

        [JilDirective(Name = "images")]
        public List<ProductImage> Images { get; set; }

        [JilDirective(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JilDirective(Name = "modified_on")]
        public DateTime? ModifiedOn { get; set; }

        public Product()
        {
            Images = new List<ProductImage>();
        }
    }
}