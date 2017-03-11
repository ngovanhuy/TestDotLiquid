using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DotLiquid.Util;
using DotLiquid.Extends.Util;

namespace DotLiquid.Extends.Filters
{
    public class ArrayFilters
    {
        /// <summary>
        /// Return the size of an array or of an string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Size(object input)
        {
            var str = input as string;
            if (str != null)
                return str.Length;

            var enumerable = input as IEnumerable;
            if (enumerable != null)
                return enumerable.Cast<object>().Count();

            return 0;
        }

        /// <summary>
		/// Join elements of the array with a certain character between them
		/// </summary>
		/// <param name="input"></param>
		/// <param name="glue"></param>
		/// <returns></returns>
		public static string Join(IEnumerable input, string glue = " ")
        {
            if (input == null)
                return null;

            IEnumerable<object> castInput = input.Cast<object>();
            return string.Join(glue, castInput);
        }

        /// <summary>
		/// Sort elements of the array
		/// provide optional property with which to sort an array of hashes or drops
		/// </summary>
		/// <param name="input"></param>
		/// <param name="property"></param>
		/// <returns></returns>
		public static IEnumerable Sort(object input, string property = null)
        {
            var enumerable = input as IEnumerable;

            var ary = enumerable != null
                ? enumerable.Flatten().Cast<object>().ToList()
                : new List<object>(new[] { input });

            if (!ary.Any())
                return ary;

            property = StringUtility.ConvertSnakeCaseToPascalCase(property);

            if (string.IsNullOrEmpty(property))
                ary.Sort();
            else if ((ary.All(o => o is IDictionary)) && ((IDictionary)ary.First()).Contains(property))
                ary.Sort((a, b) => Comparer.Default.Compare(((IDictionary)a)[property], ((IDictionary)b)[property]));
            else if (ary.All(o => o.RespondTo(property)))
                ary.Sort((a, b) => Comparer.Default.Compare(a.Send(property), b.Send(property)));

            return ary;
        }

        /// <summary>
        /// Map/collect on a given property
        /// </summary>
        /// <param name="input"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static IEnumerable Map(IEnumerable input, string property)
        {
            var values = new List<object>();

            foreach (var item in input)
            {
                var prop = item.GetType().GetProperties().FirstOrDefault(p => StringUtility.ConvertToSnakeCase(p.Name) == property);
                if (prop != null)
                    values.Add(prop.GetValue(item, null));
            }

            return values;
        }

        /// <summary>
        /// Get the first element of the passed in array 
        /// 
        /// Example:
        ///   {{ product.images | first | to_img }}
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static object First(IEnumerable array)
        {
            if (array == null)
                return null;

            return array.Cast<object>().FirstOrDefault();
        }

        /// <summary>
        /// Get the last element of the passed in array 
        /// 
        /// Example:
        ///   {{ product.images | last }}
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static object Last(IEnumerable array)
        {
            if (array == null)
                return null;

            return array.Cast<object>().LastOrDefault();
        }

        public static IEnumerable Concat(object input, IEnumerable append)
        {
            if (!IsArrayOrList(append))
            {
                throw new System.ArgumentException("concat filter requires an array argument");
            }

            var appendAtFormatList = ConverToList(append);
            var inputAtFormatList = ConverToList(input);

            if (!IsArrayOrList(append) || inputAtFormatList == null)
                return inputAtFormatList;

            var result = inputAtFormatList.Concat(appendAtFormatList).ToArray();
            return result;
        }

        private static List<object> ConverToList(object input)
        {
            if (IsArrayOrList(input))
            {
                var array = input as IEnumerable;
                return array.Flatten().Cast<object>().ToList();
            }
            List<object> result = new List<object>();
            result.Add(input);
            return result;
        }

        private static bool IsArrayOrList(object source)
        {
            return (source != null && (source.GetType().IsGenericType || source.GetType().IsArray));
        }

        public static IEnumerable Reverse(object input)
        {
            if (IsArrayOrList(input))
            {
                var array = input as IEnumerable;
                var list = array.Flatten().Cast<object>().ToList();
                list.Reverse();
                return list;
            }
            return ConverToList(input);
        }
    }
}
