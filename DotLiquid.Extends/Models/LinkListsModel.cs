using AutoMapper;
using Services.Repository;
using System.Collections;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class LinklistsModel : BaseModel, IEnumerable
    {
        private LinkedListService linklistService = new LinkedListService();
        private Dictionary<string, object> _loadedModel;

        public LinklistsModel()
        {
            _loadedModel = new Dictionary<string, object>();
        }
        public IEnumerator GetEnumerator()
        {
            if (!_loadedModel.ContainsKey("linklists"))
            {
                var linklists = linklistService.GetAll();
                var linklistsModel = Mapper.Map<List<LinklistModel>>(linklists);
                _loadedModel.Add("linklists", linklistsModel);
                return linklistsModel.GetEnumerator();
            }
            return ((List<LinklistModel>)_loadedModel["linklists"]).GetEnumerator();
        }
        public override object BeforeMethod(string singleKey)
        {
            var single = LoadSingleObject(singleKey);

            if (single != null)
                return single;

            return null;
        }

        protected LinklistModel LoadSingleObject(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                var linklist = linklistService.GetByAlias(alias);
                if (linklist != null)
                    return Mapper.Map<LinklistModel>(linklist);
            }

            return null;
        }
    }
}