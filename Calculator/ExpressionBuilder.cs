using System;
using System.Collections.Generic;

namespace Calculator
{
    internal class ExpressionBuilder
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
            var operationSymbol = expressionList.Count > 1 ? expressionList[1] : null;

            var index = 2;

            while (operationSymbol != null)
            {
                var rightNode = BuildRightNode(expressionList, ref index);
                var operation = _operationSymbol2Function[operationSymbol];

                leftNode = new OperationNode(leftNode, rightNode, operation);

                operationSymbol = expressionList.Count > index ? expressionList[index] : null;
                index++;
            }
            return leftNode;
        }

        private IExpressionNode BuildRightNode(IList<string> expressionList, ref int index)
        {
            var leftValue = int.Parse(expressionList[index]);
            var leftNode = new ConstNode(leftValue);
            var operationSymbol = expressionList.Count > index + 1 ? expressionList[index + 1] : null;

            if (operationSymbol == null || operationSymbol == "+" || operationSymbol == "-")
            {
                index++;
                return leftNode;
            }

            var operation = _operationSymbol2Function[operationSymbol];

            index += 2;
            var rightNode = BuildRightNode(expressionList, ref index);
            
            return new OperationNode(leftNode, rightNode, operation);
        }
    }
}
