using Jil;
using System;

namespace Services.Domain
{
    public class Blog
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "store_id")]
        public int StoreId { get; set; }

        [JilDirective(Name = "name")]
        public string Name { get; set; }

        [JilDirective(Name = "description")]
        public string Description { get; set; }

        [JilDirective(Name = "alias")]
        public string Alias { get; set; }

        [JilDirective(Name = "meta_title")]
        public string MetaTitle { get; set; }

        [JilDirective(Name = "meta_description")]
        public string MetaDescription { get; set; }

        [JilDirective(Name = "template_layout")]
        public string TemplateLayout { get; set; }
    }
}
