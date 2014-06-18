using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator;
using FluentAssertions;
using Moq;
using Xunit.Extensions;

namespace CalculatorTests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData("2", 2)]
        [InlineData("2+7", 9)]
        [InlineData("3*5", 15)]
        [InlineData("2+9*7-5", 60)]
        [InlineData("2+9*7/2-5", 28.5)]
        public void CalculateTest(string expression, double value)
        {
            var parsedExpression = new string[] {};
            var parserMock = new Mock<IExpressionParser>();
            parserMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(parsedExpression);

            var nodeMock = new Mock<IExpressionNode>();
            nodeMock.Setup(x => x.CalculateValue()).Returns(value);

            var builderMock = new Mock<IExpressionBuilder>();
            builderMock.Setup(x => x.BuildExpression(It.IsAny<string[]>())).Returns(nodeMock.Object);

            var calculator = new Calculator.Calculator(parserMock.Object, builderMock.Object);

            var calculatedValue = calculator.Calculate(expression);
            calculatedValue.Should().Be(value);

            parserMock.Verify(x => x.Parse(expression));
            builderMock.Verify(x => x.BuildExpression(parsedExpression));
            nodeMock.Verify(x => x.CalculateValue());
        }
    }
}
