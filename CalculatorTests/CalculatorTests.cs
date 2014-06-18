using FluentAssertions;
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
        public void CalculateIntegrationTest(string expression, double value)
        {
            var calculator = new Calculator.Calculator();

            var calculatedValue = calculator.Calculate(expression);
            calculatedValue.Should().Be(value);
        }

    }
}
