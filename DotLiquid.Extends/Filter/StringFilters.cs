using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using DotLiquid.Extends.Util;

namespace DotLiquid.Extends.Filters
{
    public class StringFilters
    {
        /// <summary>
        /// convert a input string to DOWNCASE
        /// </summary>
        public static string Downcase(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return result.ToLower();
        }

        private static string ArrayToString(string[] input)
        {
            string result = "[{0}]";

            var parts = new List<string>();
            foreach (var item in (string[])input)
            {
                parts.Add(string.Format("\"{0}\"", item));
            }

            return string.Format(result, string.Join(", ", parts));
        }

        private static string ListToString(List<string> input)
        {
            string result = "[{0}]";

            var parts = new List<string>();
            foreach (var item in input)
            {
                if (item != null)
                {
                    parts.Add(string.Format("\"{0}\"", item.ToString()));
                }
            }

            return string.Format(result, string.Join(", ", parts));
        }

        /// <summary>
        /// convert a input string to UPCASE
        /// </summary>
        public static string Upcase(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return result.ToUpper();
        }

        /// <summary>
        /// Converts a string into CamelCase.
        /// </summary>
        public static string Camelize(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
            {
                result = input as string;
                result = StringUtility.Camelize(result);
            }
            else if (input is string[])
            {
                foreach (var item in (string[])input)
                {
                    result += StringUtility.Camelize(item);
                }
            }
            else if (input.GetType().IsGenericType)
            {
                foreach (var item in (List<string>)input)
                {
                    result += StringUtility.Camelize(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a string into CamelCase.
        /// </summary>
        public static string Camelcase(object input)
        {
            return Camelize(input);
        }

        /// <summary>
        /// capitalize words in the input sentence
        /// </summary>
        public static string Capitalize(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return StringUtility.UppercaseFirst(result);
        }

        /// <summary>
        /// Escapes a string.
        /// </summary>
        public static string Escape(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            try
            {
                return WebUtility.HtmlEncode(result);
            }
            catch
            {
                return result;
            }
        }

        /// <summary>
        /// Truncates a string down to x characters
        /// </summary>
        public static string Truncate(object input, int length = 50, string truncateString = "...")
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
            {
                result = input as string;
                result = StringUtility.Truncate(result, length, truncateString);
            }
            else if (input is string[])
            {
                result = ArrayToString((string[])input);
                result = StringUtility.Truncate(result, length, truncateString);
            }
            else if (input.GetType().IsGenericType)
            {
                result = ListToString((List<string>)input);
                result = StringUtility.Truncate(result, length, truncateString);
            }
            return result;
        }

        /// <summary>
        /// Truncates a string down to 'x' words, where x is the number passed as a parameter. 
        /// An ellipsis (...) is appended to the truncated string.
        /// </summary>
        public static string Truncatewords(object input, int words = 15, string truncateString = "...")
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return StringUtility.Truncatewords(result, words, truncateString);
        }

        /// <summary>
        /// Split input string into an array of substrings separated by given pattern.
        /// </summary>
        public static string[] Split(object input, string pattern)
        {
            if (input == null)
                return new[] { string.Empty };

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);

            if (string.IsNullOrEmpty(result))
                return new[] { result };

            return result.Split(new[] { pattern }, StringSplitOptions.None);
        }

        public static object Slice(object input, int index, int length = 1)
        {
            if (input == null)
                return string.Empty;

            if (input is string)
            {
                string result = input as string;
                return !string.IsNullOrEmpty(result) ? result.Substring(index, length) : string.Empty;
            }

            if (input is string[])
            {
                var result = new List<string>();

                var array = input as string[];
                length = array.Length > length ? length : array.Length;
                for (int i = index; i < length; i++)
                {
                    result.Add(array[i]);
                }

                return result;
            }

            if (input.GetType().IsGenericType)
            {
                var result = new List<string>();

                var list = input as List<string>;
                length = list.Count > length ? length : list.Count;
                for (int i = index; i < length; i++)
                {
                    result.Add(list[i]);
                }

                return result;
            }

            return string.Empty;
        }

        /// <summary>
        /// Strips all HTML tags from a string.
        /// </summary>
        public static string StripHtml(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return StringUtility.StripHtml(result);
        }

        /// <summary>
        /// Strips tabs, spaces, and newlines (all whitespace) from the left and right side of a string.
        /// </summary>
        public static string Strip(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            result = StringUtility.StripNewlines(result);
            result = StringUtility.Strip(result);
            return result;
        }

        /// <summary>
        /// Remove all newlines from the string
        /// </summary>
        public static string StripNewlines(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return StringUtility.StripNewlines(result);
        }

        /// <summary>
        /// Replace occurrences of a string with another
        /// </summary>
        public static string Replace(object input, object @string, string replacement = "")
        {
            if (input == null)
                return string.Empty;

            if (@string == null)
                return input.ToString();

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return result.Replace(@string.ToString(), replacement);
        }

        /// <summary>
        /// Replace the first occurence of a string with another
        /// </summary>
        public static string ReplaceFirst(object input, object @string, string replacement = "")
        {
            if (input == null)
                return string.Empty;

            if (@string == null)
                return input.ToString();

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            var regex = new Regex(Regex.Escape(@string.ToString()));
            return regex.Replace(result, replacement, 1);
        }

        /// <summary>
        /// Remove a substring
        /// </summary>
        public static string Remove(object input, object @string)
        {
            return Replace(input, @string);
        }

        /// <summary>
        /// Remove the first occurrence of a substring
        /// </summary>
        public static string RemoveFirst(object input, object @string)
        {
            return ReplaceFirst(input, @string);
        }

        /// <summary>
        /// Add one string to another
        /// </summary>
        public static string Append(object input, object append)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input != null)
                result = input.ToString();

            return result + append;
        }

        /// <summary>
        /// Prepend a string to another
        /// </summary>
        public static string Prepend(object input, object append)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);
            else if (input != null)
                result = input.ToString();

            return append + result;
        }

        /// <summary>
        /// Add <br /> tags in front of all newlines in input string
        /// </summary>
        public static string NewlineToBr(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);
            else if (input != null)
                result = input.ToString();

            return Regex.Replace(result, @"(\r?\n)", "<br />$1");
        }

        /// <summary>
        /// Formats a string into a alias.
        /// </summary>
        public static string Handleize(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return StringUtility.GetSEOAlias(result);
        }

        /// <summary>
        /// Formats a string into a alias.
        /// </summary>
        public static string Handle(object input)
        {
            return Handleize(input);
        }

        /// <summary>
        /// Outputs the singular or plural version of a string based on the value of a number. 
        /// The first parameter is the singular string and the second parameter is the plural string.
        /// </summary>
        public static object Pluralize(object input, object singular, object plural)
        {
            int? @value = input as int?;
            if (@value == null)
                return plural;

            if (@value.Value > 1)
                return plural;

            return singular;
        }

        /// <summary>
        /// Identifies all characters in a string that are not allowed in URLS, and replaces the characters with their escaped variants.
        /// </summary>
        public static string UrlEscape(object input)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);

            return Uri.EscapeUriString(result);
        }

        public static object Uniq(object input)
        {
            if (input == null)
                return string.Empty;

            var array = input as IEnumerable;
            if (array == null)
                return input.ToString();

            return array.Cast<object>().Distinct();
        }
        
        public static string HmacSha256(object input, object secretKey)
        {
            if (input == null)
                return string.Empty;

            string result = string.Empty;

            if (input is string)
                result = input as string;
            else if (input is string[])
                result = ArrayToString((string[])input);
            else if (input.GetType().IsGenericType)
                result = ListToString((List<string>)input);
            else if (input != null)
                result = input.ToString();

            return ComputeHMACSHA256Hash(result, secretKey.ToString());
        }

        /// <summary>
        /// Tạo chuỗi HmacSha256
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ComputeHMACSHA256Hash(string input, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var dataBytes = Encoding.UTF8.GetBytes(input);

            var hmacSha1 = new HMACSHA256(keyBytes);
            var hmacBytes = hmacSha1.ComputeHash(dataBytes);

            return ByteToHexString(hmacBytes);
        }

        private static string ByteToHexString(byte[] data)
        {
            var sBuilder = new StringBuilder();
            foreach (byte b in data)
            {
                sBuilder.Append(b.ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
