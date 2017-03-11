using Jil;
using System;

namespace Services.Domain
{
    public class BlogPost
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "store_id")]
        public int StoreId { get; set; }

        [JilDirective(Name = "title")]
        public string Title { get; set; }

        [JilDirective(Name = "alias")]
        public string Alias { get; set; }

        [JilDirective(Name = "author")]
        public string Author { get; set; }

        [JilDirective(Name = "user_id")]
        public int? UserId { get; set; }

        [JilDirective(Name = "blog_id")]
        public int BlogId { get; set; }

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

        [JilDirective(Name = "tags")]
        public string Tags { get; set; }

        [JilDirective(Name = "content")]
        public string Content { get; set; }

        [JilDirective(Name = "summary")]
        public string Summary { get; set; }

        [JilDirective(Name = "template_layout")]
        public string TemplateLayout { get; set; }

        [JilDirective(Name = "image")]
        public MediaFile Image { get; set; }
    }
}