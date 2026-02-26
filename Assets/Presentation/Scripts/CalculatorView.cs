using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator.Presentation
{
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        [Header("Main Panel")]
        [SerializeField] private RectTransform mainPanel;

        [Header("Input")]
        [SerializeField] private TMP_InputField inputField;

        [Header("History")]
        [SerializeField] private ScrollRect historyScrollRect;
        [SerializeField] private VerticalLayoutGroup historyContent;
        [SerializeField] private GameObject historyEntryPrefab;

        [Header("Button")]
        [SerializeField] private Button resultButton;

        [Header("Dynamic Size")]
        [SerializeField] private float maxPanelHeight = 860f;

        private float _nonHistoryHeight;
        private RectTransform _hictoryContentTransform;

        public string InputText
        {
            get => inputField.text;
            private set => inputField.text = value;
        }

        public event Action OnResultClicked;

        private void Awake()
        {
            resultButton.onClick.AddListener(() => OnResultClicked?.Invoke());

            historyScrollRect.vertical = false;
            historyScrollRect.movementType = ScrollRect.MovementType.Clamped;

            historyContent.reverseArrangement = true;
            _hictoryContentTransform = historyContent.transform as RectTransform;

            LayoutRebuilder.ForceRebuildLayoutImmediate(mainPanel);
            _nonHistoryHeight = mainPanel.rect.height - LayoutUtility.GetPreferredHeight(_hictoryContentTransform);
        }

        private void UpdatePanelSize()
        {
            float contentHeight = LayoutUtility.GetPreferredHeight(_hictoryContentTransform);
            float desiredPanelHeight = _nonHistoryHeight + contentHeight;

            if (desiredPanelHeight <= maxPanelHeight)
            {
                mainPanel.sizeDelta = new (mainPanel.sizeDelta.x, desiredPanelHeight);
                var historyRect = historyScrollRect.transform as RectTransform;
                historyRect.sizeDelta = new (mainPanel.sizeDelta.x, contentHeight);
                historyScrollRect.vertical = false;
            }
            else
            {
                mainPanel.sizeDelta = new (mainPanel.sizeDelta.x, maxPanelHeight);
                historyScrollRect.vertical = true;
            }
        }

        public void AddHistoryEntry(string text)
        {
            GameObject entry = Instantiate(historyEntryPrefab, _hictoryContentTransform);
            entry.GetComponent<TMP_Text>().text = text;

            LayoutRebuilder.ForceRebuildLayoutImmediate(_hictoryContentTransform);

            UpdatePanelSize();

            Canvas.ForceUpdateCanvases();
            historyScrollRect.verticalNormalizedPosition = 1f;
        }

        public void ClearInput() => InputText = string.Empty;
        public void RestoreLastInput(string lastInput) => InputText = lastInput;
    }
}