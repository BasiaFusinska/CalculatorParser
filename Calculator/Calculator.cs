using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CalculatorTests")]

namespace Calculator
{
    public class Calculator
    {
        private readonly ExpressionParser _parser;
        private readonly ExpressionBuilder _builder;

        public Calculator()
        {
            _parser = new ExpressionParser();
            _builder = new ExpressionBuilder();
        }

        public bool CanCalculate(string expression)
        {
            return _parser.Validate(expression);
        }
        public bool TryCalculate(string expression, out double value)
        {
            value = 0;

            if (!CanCalculate(expression))
            return false;

            value = Calculate(expression);
            return true;
        }
        public double Calculate(string expression)
        {
            var expressionNode = _builder.BuildExpression(_parser.Parse(expression));
            return expressionNode.CalculateValue();
        }
    }
}
