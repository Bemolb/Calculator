using Calculator.Domain;

namespace Calculator.Application
{
    public class CalculateUseCase
    {
        public CalculationResult Execute(string input)
        {
            return CalculatorParser.TryCalculate(input, out long sum)
                ? CalculationResult.Success(input, sum)
                : CalculationResult.Error(input);
        }
    }
}