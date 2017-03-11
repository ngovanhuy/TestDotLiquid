using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace DotLiquid.Util
{
	/// <summary>
	/// Some of this code was taken from http://www.yoda.arachsys.com/csharp/miscutil/usage/genericoperators.html.
	/// General purpose Expression utilities
	/// </summary>
	public static class ExpressionUtility
    {
        private readonly static Regex SquareBracketed = new Regex(R.Q(@"^\[(.*)\]$"), RegexOptions.Compiled);
        private readonly static Regex FloatRegex = new Regex(R.Q(@"^([+-]?\d[\d\.|\,]+)$"), RegexOptions.Compiled);
        private readonly static Regex RangesRegex = new Regex(R.Q(@"^\((\S+)\.\.(\S+)\)$"), RegexOptions.Compiled);
        private readonly static Regex IntegerRegex = new Regex(R.Q(@"^([+-]?\d+)$"), RegexOptions.Compiled);
        private readonly static Regex DoubleQuotesRegex = new Regex(R.Q(@"^""(.*)""$"), RegexOptions.Compiled);
        private readonly static Regex SingleQuotesRegex = new Regex(R.Q(@"^'(.*)'$"), RegexOptions.Compiled);
        private readonly static Regex VariableParserRegex = new Regex(Liquid.VariableParser, RegexOptions.Compiled);
        /// <summary>
        /// Create a function delegate representing a binary operation
        /// </summary>
        /// <param name="body">Body factory</param>
        /// <param name="leftType"></param>
        /// <param name="rightType"></param>
        /// <param name="resultType"></param>
        /// <param name="castArgsToResultOnFailure">
        /// If no matching operation is possible, attempt to convert
        /// TArg1 and TArg2 to TResult for a match? For example, there is no
        /// "decimal operator /(decimal, int)", but by converting TArg2 (int) to
        /// TResult (decimal) a match is found.
        /// </param>
        /// <returns>Compiled function delegate</returns>
        public static Delegate CreateExpression(Func<Expression, Expression, BinaryExpression> body,
			Type leftType, Type rightType, Type resultType, bool castArgsToResultOnFailure)
		{
			ParameterExpression lhs = Expression.Parameter(leftType, "lhs");
			ParameterExpression rhs = Expression.Parameter(rightType, "rhs");
			try
			{
				try
				{
					return Expression.Lambda(body(lhs, rhs), lhs, rhs).Compile();
				}
				catch (InvalidOperationException)
				{
					if (castArgsToResultOnFailure && !( // if we show retry                                                        
						leftType == resultType && // and the args aren't
							rightType == resultType))
					{
						// already "TValue, TValue, TValue"...
						// convert both lhs and rhs to TResult (as appropriate)
						Expression castLhs = leftType == resultType ? lhs : (Expression) Expression.Convert(lhs, resultType);
						Expression castRhs = rightType == resultType ? rhs : (Expression) Expression.Convert(rhs, resultType);

						return Expression.Lambda(body(castLhs, castRhs), lhs, rhs).Compile();
					}
					throw;
				}
			}
			catch (Exception ex)
			{
				string msg = ex.Message; // avoid capture of ex itself
				return (Action)(delegate { throw new InvalidOperationException(msg); });
			}
		}


        public static object Parse(string key)
        {
            switch (key)
            {
                case null:
                case "nil":
                case "null":
                case "":
                    return null;
                case "true":
                    return true;
                case "false":
                    return false;
            }

            // Single quoted strings.
            var match = SingleQuotesRegex.Match(key);
            if (match.Success)
                return match.Groups[1].Value;

            // Double quoted strings.
            match = DoubleQuotesRegex.Match(key);
            if (match.Success)
                return match.Groups[1].Value;

            // Integer.
            match = IntegerRegex.Match(key);
            if (match.Success)
                return Convert.ToInt32(match.Groups[1].Value);

            // Ranges.
            match = RangesRegex.Match(key);
            if (match.Success)
                return Range.Inclusive(Convert.ToInt32(Parse(match.Groups[1].Value)),
                    Convert.ToInt32(Parse(match.Groups[2].Value)));

            // Floats.
            match = FloatRegex.Match(key);
            if (match.Success)
                return float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);

            return new Variable(key);
        }
    }
}