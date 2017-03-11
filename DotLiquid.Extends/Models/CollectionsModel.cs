using AutoMapper;
using Services.Domain;
using Services.Filter;
using Services.Repository;
using System.Collections;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class CollectionsModel : BaseModel, IEnumerable
    {
        private CollectionService collectionService = new CollectionService();
        private Dictionary<string, object> _loadedModel;

        public CollectionsModel()
        {
            _loadedModel = new Dictionary<string, object>();
        }

        public IEnumerator GetEnumerator()
        {
            if (!_loadedModel.ContainsKey("collections"))
            {
                var collectionFilter = new CollectionFilter();
                var collections = collectionService.Filter(collectionFilter);
                var collectionsModel = Mapper.Map<List<CollectionModel>>(collections);
                _loadedModel.Add("collections", collectionsModel);
                return collectionsModel.GetEnumerator();
            }
            return ((List<CollectionModel>)_loadedModel["collections"]).GetEnumerator();
        }

        public override object BeforeMethod(string singleKey)
        {
            var single = LoadSingleObject(singleKey);

            if (single != null)
                return single;

            return null;
        }

        protected CollectionModel LoadSingleObject(string alias)
        {
            var collection = new Collection();
            var collectionModel = new CollectionModel();

            if (!string.IsNullOrEmpty(alias))
            {
                if (!_loadedModel.ContainsKey("collections"))
                {
                    var collections = (List<CollectionModel>)_loadedModel["collections"];
                    collectionModel = collections.Find(a => a.Alias.Equals(alias));
                    _loadedModel.Add(string.Format("collections-{0}", alias), collectionModel);
                }
                else
                {
                    collection = collectionService.GetByAlias(alias);
                    if (collection != null)
                    {
                        collectionModel = Mapper.Map<CollectionModel>(collection);
                        _loadedModel.Add(string.Format("collections-{0}", alias), collectionModel);
                    }
                }
                return collectionModel;
            }

            return null;
        }
    }
}