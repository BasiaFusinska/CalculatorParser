using Calculator;
using FluentAssertions;
using Xunit.Extensions;

namespace CalculatorTests
{
    public class ExpressionTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(17)]
        [InlineData(456)]
        [InlineData(1098)]
        public void ConstNodeTest(int value)
        {
            var node = new ConstNode(value);
            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be(value);
        }

        [Theory]
        [InlineData(3, 7)]
        [InlineData(17, 6)]
        [InlineData(456, 3)]
        [InlineData(1098, 9)]
        public void AddOperationNodeTest(int left, int right)
        {
            var leftNode = new ConstNode(left);
            var rightNode = new ConstNode(right);
            var node = new OperationNode(leftNode, rightNode, (a, b) => a + b);

            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be(left + right);
        }

        [Theory]
        [InlineData(3, 7)]
        [InlineData(17, 6)]
        [InlineData(456, 3)]
        [InlineData(1098, 9)]
        public void SubstractOperationNodeTest(int left, int right)
        {
            var leftNode = new ConstNode(left);
            var rightNode = new ConstNode(right);
            var node = new OperationNode(leftNode, rightNode, (a, b) => a - b);

            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be(left - right);
        }

        [Theory]
        [InlineData(3, 7)]
        [InlineData(17, 6)]
        [InlineData(456, 3)]
        [InlineData(1098, 9)]
        public void MultiplicateOperationNodeTest(int left, int right)
        {
            var leftNode = new ConstNode(left);
            var rightNode = new ConstNode(right);
            var node = new OperationNode(leftNode, rightNode, (a, b) => a * b);

            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be(left * right);
        }

        [Theory]
        [InlineData(3, 7)]
        [InlineData(17, 6)]
        [InlineData(456, 3)]
        [InlineData(1098, 9)]
        public void DivideOperationNodeTest(int left, int right)
        {
            var leftNode = new ConstNode(left);
            var rightNode = new ConstNode(right);
            var node = new OperationNode(leftNode, rightNode, (a, b) => a / b);

            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be((double)left / right);
        }

        [Theory]
        [InlineData(3, 7, 1)]
        [InlineData(17, 6, 2)]
        [InlineData(456, 3, 8)]
        [InlineData(1098, 9, 4)]
        public void LeftNestedExpressionTest(int a, int b, int c)
        {
            var aNode = new ConstNode(a);
            var bNode = new ConstNode(b);
            var cNode = new ConstNode(c);

            var leftNode = new OperationNode(aNode, bNode, (x, y) => x + y);
            var node = new OperationNode(leftNode, cNode, (x, y) => x*y);

            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be((a + b)*c);
        }

        [Theory]
        [InlineData(3, 7, 1)]
        [InlineData(17, 6, 2)]
        [InlineData(456, 3, 8)]
        [InlineData(1098, 9, 4)]
        public void RightNestedExpressionTest(int a, int b, int c)
        {
            var aNode = new ConstNode(a);
            var bNode = new ConstNode(b);
            var cNode = new ConstNode(c);

            var rightNode = new OperationNode(bNode, cNode, (x, y) => x * y);
            var node = new OperationNode(aNode, rightNode, (x, y) => x+y);

            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be(a + b*c);
        }

        [Theory]
        [InlineData(3, 7, 1, 8)]
        [InlineData(17, 6, 2, 9)]
        [InlineData(456, 3, 8, 6)]
        [InlineData(1098, 9, 4, 67)]
        public void RightLftNestedExpressionTest(int a, int b, int c, int d)
        {
            var aNode = new ConstNode(a);
            var bNode = new ConstNode(b);
            var cNode = new ConstNode(c);
            var dNode = new ConstNode(d);

            var leftNode = new OperationNode(aNode, bNode, (x, y) => x + y);
            var rightNode = new OperationNode(cNode, dNode, (x, y) => x - y);
            var node = new OperationNode(leftNode, rightNode, (x, y) => x * y);

            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be((a + b) * (c - d));
        }

    }
}
