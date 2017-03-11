using RestSharp;
using Services.Domain;
using Services.Response;
using System;
using System.Collections.Generic;
using System.Net;

namespace Services.Repository
{
    public class ThemeService : BaseService
    {
        public List<Theme> GetAll()
        {
            try
            {
                var themes = new List<Theme>();
                var request = new RestRequest("/themes", Method.GET);
                var response = ApiClient.Execute<ThemesResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    themes = response.Data.Themes;
                    return themes;
                }
            }
            catch
            {
            }
            return null;
        }

        public Theme GetById(int id)
        {
            try
            {
                var theme = new Theme();
                var request = new RestRequest("/themes/{themeId}", Method.GET);
                request.AddUrlSegment("themeId", id.ToString());
                var response = ApiClient.Execute<ThemeResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    theme = response.Data.Theme;
                    return theme;
                }
            }
            catch
            {
            }
            return null;
        }

        public Theme GetThemeActive()
        {
            try
            {
                var store = new StoreService().GetStore();
                if (store == null || store.ActiveThemeId == 0)
                {
                    return null;
                }
                var theme = GetById(store.ActiveThemeId);
                return theme;
            }
            catch
            {
            }
            return null;
        }

    }
}