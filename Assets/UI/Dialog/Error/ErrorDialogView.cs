using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialog
{
    public class ErrorDialogView : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button gotItButton;
        [SerializeField] private string errorMsg = "Please check the expression you just entered";

        private Action _onCloseAction;

        public void Show(Action onClose)
        {
            messageText.text = errorMsg;
            _onCloseAction = onClose;

            gotItButton.onClick.AddListener(Hide);
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gotItButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);

            _onCloseAction?.Invoke();
            _onCloseAction = null;
        }
    }
}