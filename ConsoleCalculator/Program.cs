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

            var parser = new ExpressionParser();

            if (!parser.Validate(expression))
            {
                Console.WriteLine("Wrong expression");
                return;
            }

            var builder = new ExpressionBuilder();
            var expressionNode = builder.BuildExpression(parser.Parse(expression));

            Console.WriteLine("Calculated value: {0}", expressionNode.CalculateValue());
        }
    }
}
