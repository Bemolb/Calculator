using System;

namespace Calculator.Presentation
{
    public interface ICalculatorView
    {
        string InputText { get; }
        event Action OnResultClicked;
        void AddHistoryEntry(string text);
        void ClearInput();
        void RestoreLastInput(string lastInput);
    }
}