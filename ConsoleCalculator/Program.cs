using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var expression = Console.ReadLine();
            var calculator = new Calculator.Calculator();

            if (!calculator.CanCalculate(expression))
            {
                Console.WriteLine("Wrong expression");
                return;
            }

            var calculatedValue = calculator.Calculate(expression);
            Console.WriteLine("Calculated value: {0}", calculatedValue);
        }
    }
}
