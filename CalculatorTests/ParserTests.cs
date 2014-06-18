using System;
using Calculator;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace CalculatorTests
{
    public class ParserTests
    {
        [Theory]
        [InlineData("aashd", false)]
        [InlineData("2", true)]
        [InlineData("2+", false)]
        [InlineData("2+a", false)]
        [InlineData("2+3*(89/6)", false)]
        [InlineData("2+3", true)]
        [InlineData("2+3*89/6", true)]
        public void ValidateTest(string expression, bool validated)
        {
            var parser = new ExpressionParser();
            var succeded = parser.Validate(expression);

            succeded.Should().Be(validated);
        }

        [Theory]
        [InlineData("aashd")]
        [InlineData("2+")]
        [InlineData("*8-9")]
        [InlineData("2g+s5*6")]
        public void InvalidExpressionTest(string expression)
        {
            var parser = new ExpressionParser();
            Assert.Throws<ArgumentException>(() => parser.Parse(expression));
        }

        [Theory]
        [InlineData(1, "2", "2", "", "", "", "", "", "")]
        [InlineData(3, "3+9", "3", "+", "9", "", "", "", "")]
        [InlineData(5, "2+8/7", "2", "+", "8", "/", "7", "", "")]
        [InlineData(7, "2+3*89/6", "2", "+", "3", "*", "89", "/", "6")]
        public void ParseTest(int count, string expression, 
            string p1, string p2, string p3, string p4, string p5, string p6, string p7)
        {
            var parser = new ExpressionParser();
            var parsedExpression = parser.Parse(expression);

            var parameters = new [] {p1, p2, p3, p4, p5, p6, p7};

            parsedExpression.Count.Should().Be(count);

            for (var i = 0; i < count; i++)
            {
                parsedExpression[i].Should().Be(parameters[i]);
            }
        }
    }
}
