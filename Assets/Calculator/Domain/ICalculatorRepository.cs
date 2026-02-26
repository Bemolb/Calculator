using System.Collections.Generic;

namespace Calculator.Domain
{
    public interface ICalculatorRepository
    {
        (string currentInput, List<string> history) LoadState();
        void SaveState(string currentInput, List<string> history);
    }
}