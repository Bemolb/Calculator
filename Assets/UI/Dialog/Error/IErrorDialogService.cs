using System;

namespace UI.Dialog
{
    public interface IErrorDialogService
    {
        void ShowError(Action onClose);
    }
}