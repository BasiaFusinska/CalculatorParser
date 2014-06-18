using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        private readonly IExpressionParser _parser;
        private readonly IExpressionBuilder _builder;

        public Calculator(IExpressionParser parser, IExpressionBuilder builder)
        {
            _parser = parser;
            _builder = builder;
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
