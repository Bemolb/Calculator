using Calculator.Application;
using Calculator.Infrastructure;
using Calculator.Presentation;
using UI.Dialog;
using UnityEngine;

public class CalculatorBootstrap : MonoBehaviour
{
    [SerializeField] private CalculatorView view;
    [SerializeField] private ErrorDialogView dialogPrefab;
    [SerializeField] private Transform errorParent;

    private CalculatorPresenter _presenter;

    private void Start()
    {
        var repo = new PlayerPrefsCalculatorRepository();
        var useCase = new CalculateUseCase();
        var dialogService = new ErrorDialogService(dialogPrefab, errorParent);

        _presenter = new CalculatorPresenter(view, useCase, repo, dialogService);
        _presenter.Initialize();
    }

    private void OnDestroy() => _presenter?.OnDestroy();
    private void OnApplicationQuit() => _presenter?.OnDestroy();
}