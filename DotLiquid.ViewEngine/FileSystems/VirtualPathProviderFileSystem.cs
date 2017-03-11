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
using System.Text.RegularExpressions;
using System.Web.Hosting;
using DotLiquid.Exceptions;
using DotLiquid.FileSystems;
using DotLiquid.ViewEngine.Util;
using DotLiquid.ViewEngine.Extensions;
using Services.Repository;

namespace DotLiquid.ViewEngine.FileSystems
{
    public abstract class VirtualPathProviderFileSystem : IFileSystem
    {
        protected IEnumerable<string> _viewLocations;

        public abstract string FileNameFormat { get; }

        protected VirtualPathProviderFileSystem(IEnumerable<string> viewLocations)
        {
            _viewLocations = viewLocations;
        }

        public string ReadTemplateFile(Context context, string templateName)
        {
            //var templatePath = (string)context[templateName];

            //if (templatePath == null || !Regex.IsMatch(templatePath, @"^[a-zA-Z0-9_]+$"))
            //    throw new FileSystemException("Error - Illegal template name '{0}'", templatePath);

            //var checkedLocations = new List<string>();
            //var viewPath = string.Empty;
            //var viewFound = false;

            //foreach (var fullPath in _viewLocations.Select(viewLocation => Path.Combine(viewLocation, string.Format(FileNameFormat, templatePath))))
            //{
            //    if(HostingEnvironment.VirtualPathProvider.FileExists(fullPath))
            //    {
            //        viewPath = fullPath;
            //        viewFound = true;
            //        break;
            //    }

            //    checkedLocations.Add(fullPath);
            //}

            //if (!viewFound)
            //    throw new FileSystemException("Error - No such template. Looked in the following locations:<br />{0}", string.Join("<br />", checkedLocations));

            //return VirtualPathProviderHelper.Load(viewPath);
            var templatePaths = GetTemplatePaths(context, templateName);
            var themeAssetService = new ThemeAssetService();
            var themeAsset = themeAssetService.Find(templatePaths);
            return themeAsset.Value;
        }

        private List<string> GetTemplatePaths(Context context, string templateName)
        {
            var templatePath = (string)context[templateName];
            if (templatePath == null)
                throw new FileSystemException("Error - Tên template không hợp lệ: '{0}'", templateName);

            return _viewLocations.Select(viewLocation => string.Format(viewLocation, templatePath)).ToList();
        }

        public Template ReadTemplate(Context context, string templateName)
        {
            throw new NotImplementedException();
        }
    }
}
