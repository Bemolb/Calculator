using System;
using System.Text.RegularExpressions;

namespace Calculator.Domain
{
    public static class CalculatorParser
    {
        private static readonly Regex ValidEquation =
            new(@"^\d{1,18}\+\d{1,18}$", RegexOptions.Compiled);

        public static bool TryCalculate(string input, out long sum)
        {
            sum = 0;

            if (string.IsNullOrEmpty(input) || !ValidEquation.IsMatch(input))
                return false;

            var parts = input.Split('+');
            var left = parts[0];
            var right = parts[1];

            if (!long.TryParse(left, out long a) || !long.TryParse(right, out long b))
                return false;

            try
            {
                checked
                {
                    sum = a + b;
                }
                return true;
            }
            catch (OverflowException)
            {
                sum = 0;
                return false;
            }
        }
    }
}