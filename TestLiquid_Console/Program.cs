using Services.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestLiquid_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"E:\intership\dkt\project\TestLiquid\TestLiquid_Console\index.html";
            //var _html = System.IO.File.ReadAllText(path);
            //Template template = Template.Parse(_html);  // Parses and compiles the template
            //var tempBl = FindAllInstances<DotLiquid.Block>(template.Root);
            //var tempVar = FindAllInstances<DotLiquid.Variable>(template.Root);
            //string result = template.Render(Hash.FromAnonymousObject(new { name = "tobi" })); // Renders the output => "hi tobi"
            //System.Console.WriteLine(result);

            //var products = new List<Product>();
            //string result = Util.GenerateNameModelInContext(products);
            //System.Console.WriteLine(result);

            //var productService = new ProductService();
            //List<Product> products = productService.GetAll();
            //int countProduct = productService.CountAll();
            //Console.WriteLine(countProduct);

            //User user = new User
            //{
            //    String = "string 1",
            //    Number = 1,
            //    Teacher = new Teacher
            //    {
            //        Name = "name 2"
            //    }
            //};
            //List<String> temp = FindAllInstances<String>(user);

            var themeService = new ThemeService();
            var themes = themeService.GetAll();
            var themeAssetService = new ThemeAssetService();
            var themeAssets = themeAssetService.GetAll(themes.ElementAt(0).Id);
            //var themeAsset = themeAssetService.GetByKey("templates/index.bwt", themes.ElementAt(0).Id);
            System.Console.ReadKey();
        }
    }
}
