using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using Statics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelFinishedPanel : MonoBehaviour
    {
        [SerializeField] private ItemWidgetPanel itemWidgetPanel;
        [SerializeField] private Transform rewardPanel;
        [SerializeField] private Transform indicatorTransform;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private List<float> rewardLimits;
        [SerializeField] private Button skipButton;

        private float _coefficient;
        private int _collectedMoney;

        public Action onLevelFinishedPanelClosed;

        private void Start()
        {
            itemWidgetPanel.onIncreaseFinished += IncreasingFinished;
            rewardPanel.transform.localScale = Vector3.zero;
            _collectedMoney = CurrencyHandler.ThisLevelCollected;
            skipButton.onClick.AddListener(SkipButtonClicked);
        }

        private void Update()
        {
            var value = indicatorTransform.transform.localRotation.eulerAngles.z;
            if (value > 180) value -= 360;
            if (value >= rewardLimits[0] && value < rewardLimits[1])
                _coefficient = 1.5f;
            if (value >= rewardLimits[1] && value < rewardLimits[2])
                _coefficient = 2;
            if (value >= rewardLimits[2] && value < rewardLimits[3])
                _coefficient = 3;
            if (value >= rewardLimits[3] && value < rewardLimits[4])
                _coefficient = 2f;
            if (value >= rewardLimits[4] && value < rewardLimits[5])
                _coefficient = 1.5f;

            valueText.text = "+" + Utility.MinifyLong(Mathf.CeilToInt(_collectedMoney * _coefficient));
        }

        private void IncreasingFinished()
        {
            var sq = DOTween.Sequence();
            sq.Append(itemWidgetPanel.transform.DOLocalMoveY(450f, 1f));
            sq.Append(itemWidgetPanel.transform.DOScale(0.7f, 1f)).onComplete = OpenRewardPanel;
            sq.Join(DOTween.To(itemWidgetPanel.GetPaddingValue, itemWidgetPanel.SetPaddingValue,
                0.7f * itemWidgetPanel.Padding.y, 1f));
        }

        private void OpenRewardPanel()
        {
            rewardPanel.DOScale(Vector3.one, 1f);
        }

        private void SkipButtonClicked()
        {
            CurrencyHandler.ResetData();
            onLevelFinishedPanelClosed?.Invoke();
        }

        private void ResetPanel()
        {
            
        }
    }
}