using System;

namespace DotLiquid.ViewEngine.Extensions
{
    public static class CollectionExtensions
    {
        public static bool Contains(this string[] source, string member, StringComparison comparisonType = StringComparison.Ordinal)
        {
            var founds = Array.FindAll(source, s => s.Equals(member, comparisonType));
            if (founds.Length > 0)
                return true;

            return false;
        }
    }
}
