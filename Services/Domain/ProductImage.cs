using Jil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Domain
{
    public class ProductImage
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "base64")]
        public string Base64 { get; set; }

        [JilDirective(Name = "product_id")]
        public int ProductId { get; set; }

        [JilDirective(Name = "position")]
        public int Position { get; set; }

        [JilDirective(Name = "variant_ids")]
        public List<int> VariantIds { get; set; }

        [JilDirective(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JilDirective(Name = "modified_on")]
        public DateTime? ModifiedOn { get; set; }

        [JilDirective(Name = "file_name")]
        public string FileName { get; set; }

        [JilDirective(Name = "src")]
        public string Src { get; set; }

        [JilDirective(Name = "alt")]
        public string Alt { get; set; }
    }
}
