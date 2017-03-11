using AutoMapper;
using Services.Filter;
using Services.Repository;
using System.Collections;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class PagesModel : BaseModel, IEnumerable
    {
        private PageService pageService = new PageService();
        private Dictionary<string, object> _loadedModel;

        public PagesModel()
        {
            _loadedModel = new Dictionary<string, object>();
        }
        public IEnumerator GetEnumerator()
        {
            if (!_loadedModel.ContainsKey("pages"))
            {
                var pageFilter = new PageFilter
                {
                    Limit = 10,
                    Page = 1
                };
                var pages = pageService.Filter(pageFilter);
                var pagesModel = Mapper.Map<List<PageModel>>(pages);
                _loadedModel.Add("pages", pagesModel);
                return pagesModel.GetEnumerator();
            }
            return ((List<PageModel>)_loadedModel["pages"]).GetEnumerator();
        }
        public override object BeforeMethod(string singleKey)
        {
            var single = LoadSingleObject(singleKey);

            if (single != null)
                return single;

            return null;
        }

        protected PageModel LoadSingleObject(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                var linklist = pageService.GetByAlias(alias);
                if (linklist != null)
                    return Mapper.Map<PageModel>(linklist);
            }

            return null;
        }
    }
}