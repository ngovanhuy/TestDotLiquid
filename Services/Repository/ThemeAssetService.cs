using RestSharp;
using Services.Domain;
using Services.Response;
using System;
using System.Collections.Generic;
using System.Net;

namespace Services.Repository
{
    public class ThemeAssetService : BaseService
    {
        public const string DEFAULT_VIEW_PATH = "~/Views/";
        public List<ThemeAsset> GetAll(int themId)
        {
            try
            {
                var themes = new List<ThemeAsset>();
                var request = new RestRequest("/themes/{themeId}/assets", Method.GET);
                request.AddUrlSegment("themeId", themId.ToString());
                var response = ApiClient.Execute<ThemeAssetsResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    themes = response.Data.Assets;
                    return themes;
                }
            }
            catch
            {
            }
            return null;
        }

        public ThemeAsset GetByKey(string assetKey, int themeId)
        {
            try
            {
                var themeAsset = new ThemeAsset();
                var request = new RestRequest("/themes/{themeId}/assets", Method.GET);
                request.AddUrlSegment("themeId", themeId.ToString());
                request.AddParameter("key", assetKey);
                var response = ApiClient.Execute<ThemeAssetResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    themeAsset = response.Data.Asset;
                    return themeAsset;
                }
            }
            catch
            {
            }
            return null;
        }

        public string GetContent(string path)
        {
            var store = new StoreService().GetStore();
            if (store == null || store.ActiveThemeId == 0)
            {
                return null;
            }
            var themeAsset = GetByKey(path, store.ActiveThemeId);
            if (themeAsset != null)
            {
                return themeAsset.Value;
            }
            return "";
        }

        //public string GetDefaultContent(string path)
        //{
        //    string viewPath = DEFAULT_VIEW_PATH + path;
        //    var fileContents = VirtualPathProviderHelper.Load(viewPath);
        //    return fileContents;
        //}

        public ThemeAsset Find(List<string> assetKeys)
        {
            var store = new StoreService().GetStore();
            ThemeAsset asset = null;

            var request = new RestRequest("/themes/{themeId}/assets/find", Method.GET);
            request.AddUrlSegment("themeId", store.ActiveThemeId.ToString());

            if (assetKeys != null)
                request.AddParameter("keys", string.Join(",", assetKeys));

            var response = ApiClient.Execute<ThemeAssetResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    asset = response.Data.Asset;
                    break;
            }

            return asset;
        }
    }
}
