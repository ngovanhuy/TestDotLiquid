using Jil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Domain
{
    public class StaticPage
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "store_id")]
        public int StoreId { get; set; }

        [JilDirective(Name = "title")]
        public string Title { get; set; }

        [JilDirective(Name = "content")]
        public string Content { get; set; }

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

        [JilDirective(Name = "published_on")]
        public DateTime? PublishedOn { get; set; }

        [JilDirective(Name = "created_by")]
        public int CreatedBy { get; set; }

        [JilDirective(Name = "modified_by")]
        public int ModifiedBy { get; set; }

        [JilDirective(Name = "template_layout")]
        public string TemplateLayout { get; set; }

        [JilDirective(Name = "author")]
        public string Author { get; set; }
    }
}
