using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ExpressionBuilder
    {
        private readonly IDictionary<string, Func<double, double, double>> _operationSymbol2Function =
            new Dictionary<string, Func<double, double, double>>
            {
                {"+", (a, b) => a + b},
                {"-", (a, b) => a - b},
                {"*", (a, b) => a * b},
                {"/", (a, b) => a / b}
            };

        public IExpressionNode BuildExpression(IList<string> expressionList)
        {
            var leftValue = int.Parse(expressionList[0]);
            IExpressionNode leftNode = new ConstNode(leftValue);
            var functionSymbol = expressionList.Count > 1 ? expressionList[1] : null;

            var index = 2;

            while (functionSymbol != null)
            {
                var rightNode = BuildRightNode(expressionList, ref index);
                var function = _operationSymbol2Function[functionSymbol];

                leftNode = new OperationNode(leftNode, rightNode, function);

                functionSymbol = expressionList.Count > index ? expressionList[index] : null;
                index++;
            }
            return leftNode;
        }

        private IExpressionNode BuildRightNode(IList<string> expressionList, ref int index)
        {
            var leftValue = int.Parse(expressionList[index]);
            var leftNode = new ConstNode(leftValue);
            var functionSymbol = expressionList.Count > index + 1 ? expressionList[index + 1] : null;

            if (functionSymbol == null || functionSymbol == "+" || functionSymbol == "-")
            {
                index++;
                return leftNode;
            }

            var function = _operationSymbol2Function[functionSymbol];

            index += 2;
            var rightNode = BuildRightNode(expressionList, ref index);
            
            return new OperationNode(leftNode, rightNode, function);
        }
    }
}
