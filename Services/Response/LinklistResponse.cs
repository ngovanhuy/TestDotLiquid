using Jil;
using Services.Domain;

namespace Services.Response
{
    public class LinklistResponse
    {
        [JilDirective(Name = "linklist")]
        public Linklist Linklist { get; set; }
    }
}
