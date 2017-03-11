using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DotLiquid.Extends.Util
{
    public class StringUtility
    {
        private readonly static Regex FloatRegex = new Regex(@"^([+-]?\d[\d\.|\,]+)$", RegexOptions.Compiled);
        private readonly static Regex IntegerRegex = new Regex(@"^([+-]?\d+)$", RegexOptions.Compiled);

        /// <summary>
        /// Tạo Alias cho một chuỗi để SEO Url
        /// Ví dụ: Sàn giao dịch HàngTốt.com => san-giao-dich-hangtot-com
        /// </summary>
        /// <param name="input"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GetSEOAlias(string input, int maxLength = 0)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            input = RemoveVietnameseChar(input);
            input = Regex.Replace(input, @"[^A-Za-z0-9]", "-");
            input = input.Trim('-');
            input = Regex.Replace(input, @"\-+", "-");
            if (maxLength > 80 || maxLength <= 0)
            {
                maxLength = 80;
            }
            if (maxLength > 0 && input.Length > maxLength)
            {
                input = input.Substring(0, maxLength);
            }
            return input.ToLower();
        }

        /// <summary>
        /// Chuyển từ tiếng Việt có dấu => không dấu
        /// Ví dụ: Sàn giao dịch HàngTốt.com => San giao dich HangTot.com
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveVietnameseChar(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            string strResult = Regex.Replace(input, "[à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ]", "a");
            strResult = Regex.Replace(strResult, "[è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ]", "e");
            strResult = Regex.Replace(strResult, "[ì|í|ị|ỉ|ĩ]", "i");
            strResult = Regex.Replace(strResult, "[ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ]", "o");
            strResult = Regex.Replace(strResult, "[ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ]", "u");
            strResult = Regex.Replace(strResult, "[ỳ|ý|ỵ|ỷ|ỹ]", "y");
            strResult = Regex.Replace(strResult, "[đ]", "d");
            strResult = Regex.Replace(strResult, "[À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ]", "A");
            strResult = Regex.Replace(strResult, "[È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ]", "E");
            strResult = Regex.Replace(strResult, "[Ì|Í|Ị|Ỉ|Ĩ]", "I");
            strResult = Regex.Replace(strResult, "[Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ]", "O");
            strResult = Regex.Replace(strResult, "[Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ]", "U");
            strResult = Regex.Replace(strResult, "[Ỳ|Ý|Ỵ|Ỷ|Ỹ]", "Y");
            strResult = Regex.Replace(strResult, "[Đ]", "D");
            return strResult;
        }

        public static string ConvertToSnakeCase(string input)
        {
            var regex1 = new Regex(@"([A-Z]+)([A-Z][a-z])");
            var regex2 = new Regex(@"([a-z\d])([A-Z])");

            return regex2.Replace(regex1.Replace(input, "$1_$2"), "$1_$2").ToLower();
        }

                /// <summary>
        /// Converts a string into CamelCase.
        /// </summary>
        public static string Camelize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            input = Regex.Replace(input, @"\s+|\+|\-|\/|\\", " ");
            input = Regex.Replace(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input), @"\s+", "");
            return input;
        }

        /// <summary>
        /// Truncates a string down to x characters
        /// </summary>
        public static string Truncate(string input, int length = 50, string truncateString = "...")
        {
            if (string.IsNullOrEmpty(input))
                return input;

            int l = length - truncateString.Length;

            return input.Length > length ? input.Substring(0, l < 0 ? 0 : l) + truncateString : input;
        }

        public static string ConvertSnakeCaseToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
                        .Aggregate(string.Empty, (s1, s2) => s1 + s2);
        }

        public static string UppercaseFirst(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            input = input.ToLower();
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// Truncates a string down to 'x' words, where x is the number passed as a parameter. 
        /// An ellipsis (...) is appended to the truncated string.
        /// </summary>
        public static string Truncatewords(string input, int words = 15, string truncateString = "...")
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var wordList = input.Split(' ').ToList();
            int l = words < 0 ? 0 : words;

            return wordList.Count > l ? string.Join(" ", wordList.Take(l).ToArray()) + truncateString : input;
        }
        /// <summary>
        /// Remove all Html Tags, new line, tabs, spaces and return the subtracted content
        /// depends on passed length.
        /// </summary>
        public static string SubtractMetaDescription(string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            input = StripHtml(input);
            input = StripNewlines(input);
            input = input.Replace("\"", string.Empty);
            input = input.Replace("\'", string.Empty);
            input = Strip(input);
            return input.Length > length ? input.Substring(0, length) : input;
        }

        /// <summary>
        /// Strip all space.
        /// </summary>
        public static string Strip(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            input = Regex.Replace(input, @"\s+", " ");
            input = input.TrimEnd();
            input = input.TrimStart();
            return HttpUtility.HtmlDecode(input);
        }

        /// <summary>
        /// Strip all Html tag.
        /// </summary>
        public static string StripHtml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            input = Regex.Replace(input, @"<[^>]+>|&nbsp;", " ");
            return HttpUtility.HtmlDecode(input);
        }

        /// <summary>
        /// Strip all new line and tabs.
        /// </summary>
        public static string StripNewlines(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            input = Regex.Replace(input, @"(\r?\n)", " ");
            return HttpUtility.HtmlDecode(input);
        }

        public static string JoinString(params string[] args)
        {
            var builder = new StringBuilder();

            foreach (var item in args)
            {
                builder.Append(item);
            }

            return builder.ToString();
        }

        public static string BeautyHtmlLineBreak(string description)
        {
            if (string.IsNullOrEmpty(description))
                return description;

            var paragraphs = description.Split('\n');
            return string.Join(" ", paragraphs
                .Select(p => p != "\r" ? string.Format("<p>{0}</p>", p) : "<br />")
                .ToArray()
            );
        }


        /// <summary>
        /// Parser string to value depends on it value type(example: float, int,...)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object ParserValue(string input)
        {
            var match = IntegerRegex.Match(input);
            if (match.Success)
                return Convert.ToInt32(match.Groups[1].Value);

            match = FloatRegex.Match(input);
            if (match.Success)
                return float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);

            return input;
        }
    }
}