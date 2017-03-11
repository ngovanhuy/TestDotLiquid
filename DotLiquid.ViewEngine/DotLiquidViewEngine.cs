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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DotLiquid.ViewEngine.FileSystems;
using DotLiquid.ViewEngine.Tags;

namespace DotLiquid.ViewEngine
{
    public class DotLiquidViewEngine : VirtualPathProviderViewEngine
    {
        public DotLiquidViewEngine()
        {
            /* {0} = view name or master page name
             * {1} = controller name
             */

            // create our master page location
            MasterLocationFormats = new[] {
                //"~/Views/{1}/_{0}.bwt",
                //"~/Views/Shared/_{0}.bwt"
                "layouts/{0}.bwt"
            };

            // create our views and common shared locations
            ViewLocationFormats = new[] {
                //"~/Views/{1}/{0}.bwt",
                //"~/Views/Shared/{0}.bwt"
                "templates/{0}.bwt"
            };

            // create our partial views and common shared locations
            PartialViewLocationFormats = new[] {
                "snippets/{0}.bwt"
                //"~/Views/{1}/_{0}.bwt",
                //"~/Views/Shared/_{0}.bwt"
            };

            // Register custom tags (Only need to do this once)
            Template.RegisterTag<UrlTag>("url");
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            Template.FileSystem = new DotLiquidFileSystem(MasterLocationFormats.Concat(PartialViewLocationFormats));

            return new DotLiquidView(controllerContext, partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            Template.FileSystem = new DotLiquidFileSystem(MasterLocationFormats.Concat(PartialViewLocationFormats));

            return new DotLiquidView(controllerContext, viewPath, masterPath);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {

            return new ViewEngineResult(CreateView(controllerContext, viewName, ""), this);
        }
    }
}
