namespace Calculator
{
    public class ConstNode : IExpressionNode
    {
        private readonly int _value;

        public ConstNode(int value)
        {
            _value = value;
        }

        public double CalculateValue()
        {
            return _value;
        }
    }
}
