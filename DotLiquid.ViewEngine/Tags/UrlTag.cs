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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotLiquid.Exceptions;
using DotLiquid.Util;
using DotLiquid.ViewEngine.Extensions;

namespace DotLiquid.ViewEngine.Tags
{
    public class UrlTag : Tag
    {
        private static readonly Regex Syntax = R.B(R.Q(@"(?<action>{0}+)(\s(?<controller>{0}+))?(\s(?<area>{0}+))?"), Liquid.QuotedFragment);

        private string _actionName;
        private string _controllerName;
        private string _areaName;

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            var syntaxMatch = Syntax.Match(markup);
            if (syntaxMatch.Success)
            {
                _actionName = syntaxMatch.Groups["action"].Value.TrimQuotes();
                _controllerName = syntaxMatch.Groups["controller"].Value.TrimQuotes();
                _areaName = syntaxMatch.Groups["area"].Value.TrimQuotes();
            }
            else
            {
                throw new SyntaxException("");
            }

            base.Initialize(tagName, markup, tokens);
        }

        public override void Render(Context context, System.IO.TextWriter result)
        {
            var httpContext = HttpContext.Current;
            if(httpContext == null)
                throw new InvalidOperationException("The link tag can only be used within a valid HttpContext");

            var httpContextBase = new HttpContextWrapper(httpContext);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContextBase, routeData);

            var urlHelper = new UrlHelper(requestContext);

            if(!string.IsNullOrEmpty(_controllerName))
            {
                if(!string.IsNullOrEmpty(_areaName))
                {
                    result.Write(urlHelper.Action(_actionName, _controllerName, new { Area = _areaName }));
                    return;
                }

                result.Write(urlHelper.Action(_actionName, _controllerName));
                return;
            }

            result.Write(urlHelper.Action(_actionName));
        }
    }
}
