using Jil;
using System;
using System.Collections.Generic;

namespace Services.Domain
{
    public class Collection
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

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

        [JilDirective(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JilDirective(Name = "modified_on")]
        public DateTime? ModifiedOn { get; set; }

        [JilDirective(Name = "template_layout")]
        public string TemplateLayout { get; set; }

        [JilDirective(Name = "sort_order")]
        public string SortOrder { get; set; }

        [JilDirective(Name = "published_on")]
        public DateTime? PublishedOn { get; set; }

        [JilDirective(Name = "image")]
        public MediaFile Image { get; set; }

        [JilDirective(Name = "disjunctive")]
        public bool Disjunctive { get; set; }

        [JilDirective(Name = "type")]
        public string Type { get; set; }

        [JilDirective(Name = "rules")]
        public List<CollectionRule> Rules { get; set; }

        public Collection()
        {
            Rules = new List<CollectionRule>();
        }
    }
}
