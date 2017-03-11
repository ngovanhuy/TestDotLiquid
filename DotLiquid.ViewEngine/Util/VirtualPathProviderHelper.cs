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
using System.IO;
using System.Web.Caching;
using System.Web.Hosting;

namespace DotLiquid.ViewEngine.Util
{
    public static class VirtualPathProviderHelper
    {
        //public static Func<string, Action, string> LoadAndMonitor = DefaultLoadAndMonitor;
        
        //public static string DefaultLoadAndMonitor(string virtualPath, Action changed)
        //{
        //    Monitor(virtualPath, changed);

        //    return Load(virtualPath);
        //}

        //public static void Monitor(string virtualPath, Action changed)
        //{
        //    try
        //    {
        //        HostingEnvironment.Cache.Add(
        //            Guid.NewGuid().ToString("n"),
        //            string.Empty,
        //            HostingEnvironment.VirtualPathProvider.GetCacheDependency(
        //                virtualPath,
        //                new[] {virtualPath},
        //                DateTime.UtcNow),
        //            Cache.NoAbsoluteExpiration,
        //            Cache.NoSlidingExpiration,
        //            CacheItemPriority.High,
        //            (_, __, reason) => changed());
        //    }
        //    catch
        //    {
        //        // silent failing on Monitor is expected
        //    }
        //}

        public static string Load(string virtualPath)
        {
            if (!HostingEnvironment.VirtualPathProvider.FileExists(virtualPath))
                return null;

            try
            {
                var virtualFile = HostingEnvironment.VirtualPathProvider.GetFile(virtualPath);
                using (var stream = virtualFile.Open())
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            
        }
    }
}
