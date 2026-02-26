namespace Calculator.Application
{
    public class CalculationResult
    {
        public bool IsSuccess { get; }
        public string Input { get; }
        public string Output { get; }

        private CalculationResult(bool success, string input, string output)
        {
            IsSuccess = success;
            Input = input;
            Output = output;
        }

        public static CalculationResult Success(string input, long result) => new(true, input, result.ToString());
        public static CalculationResult Error(string input) => new(false, input, "ERROR");
    }
}