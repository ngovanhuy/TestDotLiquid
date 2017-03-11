using AutoMapper;
using Services.Filter;
using Services.Repository;
using System.Collections;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class ProductsModel : BaseModel, IEnumerable
    {
        private ProductService productService = new ProductService();
        private Dictionary<string, object> _loadedModel;
        private CollectionModel collectionModel;

        public ProductsModel()
        {
            this.collectionModel = null;
            _loadedModel = new Dictionary<string, object>();
        }

        public ProductsModel(CollectionModel collectionModel)
        {
            this.collectionModel = collectionModel;
            _loadedModel = new Dictionary<string, object>();
        }

        public IEnumerator GetEnumerator()
        {
            if (!_loadedModel.ContainsKey("all_products"))
            {
                var productFilter = new ProductFilter
                {
                    Page = 1,
                    Limit = 20
                };

                if (collectionModel != null && collectionModel.Id != CollectionModel.ALL_PRODUCTS_COLLECTION_ID)
                    productFilter.CollectionId = collectionModel.Id;
                var products = productService.Filter(productFilter);
                var productModels = Mapper.Map<List<ProductModel>>(products);
                _loadedModel.Add("all_products", productModels);
                return productModels.GetEnumerator();
            }
            return ((List<ProductModel>)_loadedModel["all_products"]).GetEnumerator();
        }

        public virtual int Count
        {
            get
            {
                if (!_loadedModel.ContainsKey("productsCount"))
                {
                    var productFilter = new ProductFilter
                    {
                    };

                    if (collectionModel.Id != CollectionModel.ALL_PRODUCTS_COLLECTION_ID)
                        productFilter.CollectionId = collectionModel.Id;
                    var products = productService.FilterCount(productFilter);
                    _loadedModel.Add("productsCount", products);
                }
                return (int)_loadedModel["productsCount"];
            }
        }
    }
}