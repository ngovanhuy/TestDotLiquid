using DotLiquid;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace TestLiquid.CustomView
{
    public class MyView : IView
    {
        private string _viewPhysicalPath;

        public MyView(string ViewPhysicalPath)
        {
            _viewPhysicalPath = ViewPhysicalPath;
        }

        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            string rawcontents = File.ReadAllText(_viewPhysicalPath);

            // Copy data from the view context over to DotLiquid
            var localVars = new Hash();
            if (viewContext.ViewData.Model != null)
            {
                var liquidContext = (Dictionary<string, object>)viewContext.ViewData.Model;
                foreach (var key in liquidContext.Keys)
                {
                    localVars.Add(key, liquidContext[key]);
                }

                //if (model.GetType().Name.EndsWith("ViewModel"))
                //{
                //    // If it's view model, just copy all properties to the localVars collection
                //    localVars.Merge(Hash.FromAnonymousObject(model));
                //}
                //else
                //{
                    // It it's not a view model, just add the model direct as a "model" variable
                    
                //}
            }
            foreach (var item in viewContext.ViewData)
                localVars.Add(Template.NamingConvention.GetMemberName(item.Key), item.Value);

            foreach (var item in viewContext.TempData)
                localVars.Add(Template.NamingConvention.GetMemberName(item.Key), item.Value);

            var renderParams = new RenderParameters
            {
                LocalVariables = Hash.FromDictionary(localVars)
            };

            Template template = Template.Parse(rawcontents);
            template.Render(writer, renderParams);
        }
    }

}