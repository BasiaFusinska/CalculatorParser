using System.Linq;
using Calculator;
using FluentAssertions;
using Xunit.Extensions;

namespace CalculatorTests
{
    public class ExpressionBuilderTests
    {
        [Theory]
        [InlineData(1, 2, "2", "", "", "", "", "", "")]
        [InlineData(3, 12, "3", "+", "9", "", "", "", "")]
        [InlineData(5, 4.5, "2", "+", "5", "/", "2", "", "")]
        [InlineData(7, 46.5, "2", "+", "3", "*", "89", "/", "6")]
        [InlineData(7, 20, "2", "+", "3", "*", "8", "-", "6")]
        public void BuildExpressionTest(int count, double value, string p1, string p2, string p3, string p4, string p5, string p6, string p7)
        {
            var parameters = new[] { p1, p2, p3, p4, p5, p6, p7 };
            var builder = new ExpressionBuilder();

            var node = builder.BuildExpression(parameters.Where((x, i) => i < count).ToList());
            var calculatedValue = node.CalculateValue();

            calculatedValue.Should().Be(value);
        }
    }
}
