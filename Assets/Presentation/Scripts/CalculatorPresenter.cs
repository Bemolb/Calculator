using Calculator.Application;
using Calculator.Domain;
using System.Collections.Generic;
using UI.Dialog;

namespace Calculator.Presentation
{
    public class CalculatorPresenter
    {
        private readonly ICalculatorView _view;
        private readonly CalculateUseCase _useCase;
        private readonly ICalculatorRepository _repository;
        private readonly IErrorDialogService _dialogService;

        private string _lastFailedInput;
        private List<string> _history = new ();

        public CalculatorPresenter(ICalculatorView view, CalculateUseCase useCase,
            ICalculatorRepository repository, IErrorDialogService dialogService)
        {
            _view = view;
            _useCase = useCase;
            _repository = repository;
            _dialogService = dialogService;

            _view.OnResultClicked += OnResultClicked;
        }

        public void Initialize()
        {
            var (input, history) = _repository.LoadState();

            _history = history;
            _view.RestoreLastInput(input);

            foreach (var entryText in _history)
                _view.AddHistoryEntry(entryText);
        }

        private void OnResultClicked()
        {
            var input = _view.InputText.Trim();

            if (string.IsNullOrEmpty(input)) 
                return;

            var result = _useCase.Execute(input);
            var formatted = string.Empty;

            if (result.IsSuccess)
            {
                formatted = $"{result.Input}={result.Output}";
                _lastFailedInput = null;
                _view.ClearInput();
            }
            else
            {
                formatted = $"{result.Input}=ERROR";
                _lastFailedInput = result.Input;
                _dialogService.ShowError(OnDialogClosed);
            }

            _history.Add(formatted);
            _view.AddHistoryEntry(formatted);

            _repository.SaveState(_view.InputText, _history);
        }

        private void OnDialogClosed()
        {
            if (!string.IsNullOrEmpty(_lastFailedInput))
                _view.RestoreLastInput(_lastFailedInput);
        }

        public void OnDestroy() => _repository.SaveState(_view.InputText, _history);
    }
}