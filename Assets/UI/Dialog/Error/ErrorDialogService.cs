using UnityEngine;

namespace UI.Dialog
{
    public class ErrorDialogService : IErrorDialogService
    {
        private readonly ErrorDialogView _prefab;
        private readonly Transform _parent;
        private ErrorDialogView _instance;

        public ErrorDialogService(ErrorDialogView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public void ShowError(System.Action onClose)
        {
            if (_instance == null)
                _instance = Object.Instantiate(_prefab, _parent);

            _instance.Show(onClose);
        }
    }
}