using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotLiquid.Extends.Util
{
    public class MediaUtility
    {
        public static string GenerateS3FolderFromStoreId(int storeId)
        {
            var pathBuilder = new StringBuilder();

            storeId = storeId + 100000000;
            int firstParam = storeId % 1000;
            int secondValue = storeId / 1000;
            int secondParam = secondValue % 1000;

            int thirdValue = secondValue / 1000;
            if (thirdValue < 1000)
                pathBuilder.Append(thirdValue.ToString("D3"));
            else
                pathBuilder.Append(thirdValue);

            pathBuilder.Append("/");
            pathBuilder.Append(secondParam.ToString("D3"));
            pathBuilder.Append("/");
            pathBuilder.Append(firstParam.ToString("D3"));

            return pathBuilder.ToString();
        }
    }
}
