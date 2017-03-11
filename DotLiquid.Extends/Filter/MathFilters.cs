using DotLiquid.Extends.Util;
using System;

namespace DotLiquid.Extends.Filters
{
    public class MathFilters
    {
        public static object Modulo(object input, object operand)
        {
            return MathUtility.DoOperation(Operator.Modulo, input, operand);
        }

        public static object DividedBy(object input, object operand)
        {
            return MathUtility.DoOperation(Operator.Devided, input, operand);
        }

        public static object Plus(object input, object operand)
        {
            return MathUtility.DoOperation(Operator.Plus, input, operand);
        }

        public static object Minus(object input, object operand)
        {
            return MathUtility.DoOperation(Operator.Minus, input, operand);
        }

        public static object Times(object input, object operand)
        {
            return MathUtility.DoOperation(Operator.Times, input, operand);
        }

        public static object Ceil(object input)
        {
            if (input is int)
                return (int)input;
            if (input is float)
                return Math.Ceiling((float)input);
            if (input is decimal)
                return Math.Ceiling((decimal)input);
            if (input is string)
            {
                object value = StringUtility.ParserValue(input.ToString());
                if (value is int)
                    return (int)value;
                if (value is float)
                    return Math.Ceiling((float)value);
            }

            return 0;
        }

        public static object Floor(object input)
        {
            if (input is int)
                return (int)input;
            if (input is float)
                return Math.Floor((float)input);
            if (input is decimal)
                return Math.Floor((decimal)input);
            if (input is string)
            {
                object value = StringUtility.ParserValue(input.ToString());
                if (value is int)
                    return (int)value;
                if (value is float)
                    return Math.Floor((float)value);
            }

            return 0;
        }

        public static object Round(object input, int decimals = 0)
        {
            if (input is int)
                return (int)input;
            if (input is float)
                return Math.Round((float)input, decimals);
            if (input is decimal)
                return Math.Round((decimal)input, decimals);
            if (input is string)
            {
                object value = StringUtility.ParserValue(input.ToString());
                if (value is int)
                    return (int)value;
                if (value is float)
                    return Math.Round((float)value, decimals);
            }

            return 0;
        }

        public static object Abs(object input)
        {
            if (input is int)
                return Math.Abs((int)input);
            if (input is float)
                return Math.Abs((float)input);
            if (input is decimal)
                return Math.Abs((decimal)input);
            if (input is string)
            {
                object value = StringUtility.ParserValue(input.ToString());
                if (value is int)
                    return Math.Abs((int)value);
                if (value is float)
                    return Math.Abs((float)value);
            }

            return 0;
        }
    }
}
