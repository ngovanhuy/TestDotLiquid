namespace DotLiquid.Extends.Util
{
    public class MathUtility
    {
        public static object DoOperation(Operator op, object input, object operand)
        {
            if (input is string)
                input = StringUtility.ParserValue(input.ToString());

            if (operand is string)
                operand = StringUtility.ParserValue(operand.ToString());

            return DoTheCSharpOperation(op, input, operand);
        }

        public static object DoTheCSharpOperation(Operator op, dynamic input, dynamic operand)
        {
            switch (op)
            {
                case Operator.Plus:
                    return input + operand;
                case Operator.Minus:
                    return input - operand;
                case Operator.Devided:
                    if ((decimal)operand == 0)
                        return 0;

                    return input / operand;
                case Operator.Modulo:
                    if ((decimal)operand == 0)
                        return 0;

                    return input % operand;
                case Operator.Times:
                    return input * operand;
                default:
                    return string.Empty;
            }
        }
    }

    public enum Operator
    {
        Plus,
        Minus,
        Devided,
        Modulo,
        Times
    }
}
