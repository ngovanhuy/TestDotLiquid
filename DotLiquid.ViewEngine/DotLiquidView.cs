/* Copyright (C) 2012 by Matt Brailsford

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DotLiquid.FileSystems;
using DotLiquid.ViewEngine.FileSystems;
using DotLiquid.ViewEngine.Tags;
using DotLiquid.ViewEngine.Util;
using DotLiquid.ViewEngine.Extensions;
using Services.Repository;
using DotLiquid.Tags;

namespace DotLiquid.ViewEngine
{
    public class DotLiquidView : IView
    {
        private ControllerContext _controllerContext;

        public string MasterPath { get; protected set; }
        public string ViewPath { get; protected set; }
        public static readonly string[] EditableTemplateViews = {
            "templates/index.bwt",
            "templates/blog.bwt",
            "templates/article.bwt",
            "templates/collection.bwt",
            "templates/list_collections.bwt",
            "templates/product.bwt",
            "templates/page.bwt",
            "templates/cart.bwt",
            "templates/search.bwt",
            "templates/404.bwt",
            "templates/customers/account.bwt",
            "templates/customers/addresses.bwt",
            "templates/customers/register.bwt",
            "templates/customers/login.bwt",
            "templates/customers/order.bwt",
            "templates/customers/reset_password.bwt",
            "templates/password.bwt"
        };
        public const string DEFAULT_LAYOUT = "theme";
        public const string CONTENT_FOR_HEADER = "content_for_header";
        public const string CONTENT_FOR_LAYOUT = "content_for_layout";

        public DotLiquidView(ControllerContext controllerContext,
            string partialPath)
            : this(controllerContext, partialPath, null)
        { }

        public DotLiquidView(ControllerContext controllerContext,
            string viewPath, string masterPath)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            if (string.IsNullOrEmpty(viewPath))
                throw new ArgumentNullException("viewPath");

            _controllerContext = controllerContext;

            ViewPath = viewPath;
            MasterPath = masterPath;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            if (viewContext == null)
                throw new ArgumentNullException("viewContext");

            // Copy data from the view context over to DotLiquid
            var localVars = new Hash();

            if (viewContext.ViewData.Model != null)
            {
                var liquidContext = (Dictionary<string, object>)viewContext.ViewData.Model;
                foreach (var key in liquidContext.Keys)
                {
                    localVars.Add(key, liquidContext[key]);
                }

                //var model = viewContext.ViewData.Model;

                //if(model.GetType().Name.EndsWith("ViewModel"))
                //{
                //    // If it's view model, just copy all properties to the localVars collection
                //    localVars.Merge(Hash.FromAnonymousObject(model));
                //}
                //else
                //{
                // It it's not a view model, just add the model direct as a "model" variable
                //localVars.Add("model", model);
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

            // Render the template
            //var fileContents = VirtualPathProviderHelper.Load(ViewPath);
            //var template = Template.Parse(fileContents);
            var template = FindTemplate(ViewPath);
            string result = Render(template, renderParams);
            writer.Write(result);
            //template.Render(writer, renderParams);
        }

        private string Render(Template template, RenderParameters renderParams)
        {
            var layoutTemplate = FindLayoutTemplate(template);
            renderParams.LocalVariables["content_for_layout"] = template.Render(renderParams);
            return layoutTemplate.Render(renderParams);
        }

        private string FindLayoutName(Template viewTemplate)
        {
            string layoutName = DEFAULT_LAYOUT;

            if (viewTemplate.Root.NodeList.Count > 0)
            {
                var firstTag = viewTemplate.Root.NodeList[0] as Layout;
                if (firstTag != null)
                {
                    if (!string.IsNullOrEmpty(firstTag.TemplateNameExpr))
                        layoutName = firstTag.TemplateNameExpr;
                    else
                        layoutName = null;
                }
            }

            return layoutName;
        }

        private Template FindTemplate(string templatePath)
        {
            Template template = null;
            string contentTemplate = "";
            if (IsEditableTemplate(templatePath))
            {
                var themeAssetService = new ThemeAssetService();
                contentTemplate = themeAssetService.GetContent(templatePath);
            }
            else
            {
                contentTemplate = VirtualPathProviderHelper.Load(templatePath);
            }
            if (!string.IsNullOrEmpty(contentTemplate))
            {
                template = Template.Parse(contentTemplate);
            }
            if (template == null)
                template = Template.Parse(string.Format("Không tìm thấy template '{0}'", templatePath));
            return template;
        }

        private Template FindLayoutTemplate(Template viewTemplate)
        {
            Template template = null;
                string layoutName = FindLayoutName(viewTemplate);
            if (!string.IsNullOrEmpty(layoutName))
            {
                string layoutPath = FindLayoutPath(layoutName);
                template = FindTemplate(layoutPath);
                if (template == null)
                    template = Template.Parse(string.Format("Không tìm thấy layout '{0}'", layoutName));
                else if (!HasContentForHeaderTag(template))
                    template = Template.Parse(string.Format("Không tìm thấy thẻ '{0}'", CONTENT_FOR_HEADER));
            }
            return template;
        }

        private string FindLayoutPath(string layoutName)
        {
            return string.Format("layouts/{0}.bwt", layoutName);
        }

        private bool IsEditableTemplate(string path)
        {
            if (path.Contains("layouts/"))
                return true;
            bool temp = EditableTemplateViews.Contains(path, StringComparison.OrdinalIgnoreCase);
            return temp;
        }

        private bool HasContentForHeaderTag(Template template)
        {
            var variableNodes = template.Root.NodeList.Where(n => n is Variable).Select(n => n as Variable).ToList();
            return variableNodes.Any(n => n.Name.ToLower() == CONTENT_FOR_HEADER);
        }
    }
}
