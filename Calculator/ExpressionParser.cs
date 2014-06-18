using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator
{
    internal class ExpressionParser
    {
        public bool Validate(string expression)
        {
            const string pattern = @"^\s*(?<left>\d+)\s*((?<operator>(\+|\-|\*|\/))\s*(?<right>\d+)\s*)*$";
            return Regex.IsMatch(expression, pattern, RegexOptions.IgnorePatternWhitespace);
        }

        public IList<string> Parse(string expression)
        {
            if (!Validate(expression)) throw new ArgumentException("Invalid expresion", "expression");

            const string left = "left";
            const string @operator = "operator";
            const string right = "right";
            const string pattern = @"^\s*(?<left>\d+)\s*((?<operator>(\+|\-|\*|\/))\s*(?<right>.*)\s*){0,1}$";

            var parsedExpression = new List<string>();

            string operatorText;

            do
            {
                var match = Regex.Match(expression, pattern, RegexOptions.IgnorePatternWhitespace);

                var leftText = match.Groups[left].Value;
                operatorText = match.Groups[@operator].Value;
                expression = match.Groups[right].Value;

                parsedExpression.Add(leftText);
                if (!string.IsNullOrEmpty(operatorText))
                    parsedExpression.Add(operatorText);

            }
            while (!string.IsNullOrEmpty(operatorText));

            return parsedExpression;
        }

        public bool TryParse(string expression, out IList<string> parsedExpression)
        {
            parsedExpression = null;
            if (!Validate(expression)) return false;

            parsedExpression = Parse(expression);
            return true;
        }
    }
}
