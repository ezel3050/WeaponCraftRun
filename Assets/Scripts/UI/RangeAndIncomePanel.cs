using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RangeAndIncomePanel : MonoBehaviour
    {
        [SerializeField] private Button continueBtn;

        public Action onRangeAndIncomePanelClosed;

        private void Start()
        {
            continueBtn.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            onRangeAndIncomePanelClosed?.Invoke();
        }
    }
}