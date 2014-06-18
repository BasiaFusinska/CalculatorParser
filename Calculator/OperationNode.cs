using System;

namespace Calculator
{
    public class OperationNode: IExpressionNode
    {
        private readonly IExpressionNode _left;
        private readonly IExpressionNode _right;
        private readonly Func<double, double, double> _operation;

        public OperationNode(IExpressionNode left, IExpressionNode right, Func<double, double, double> operation)
        {
            _left = left;
            _right = right;
            _operation = operation;
        }

        public double CalculateValue()
        {
            return _operation(_left.CalculateValue(), _right.CalculateValue());
        }
    }
}
