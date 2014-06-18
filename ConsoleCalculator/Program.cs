using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator;

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
