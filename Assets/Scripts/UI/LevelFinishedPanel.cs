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
        [SerializeField] private Button videoButton;
        [SerializeField] private Button skipButton;

        private float _coefficient;
        private int _collectedMoney;
        private int _currentIntValue;

        public Action<int> onLevelFinishedPanelClosed;
        public Action onGloveReady;

        private void Start()
        {
            itemWidgetPanel.onIncreaseFinished += IncreasingFinished;
            rewardPanel.transform.localScale = Vector3.zero;
            _collectedMoney = CurrencyHandler.ThisLevelCollected;
            skipButton.onClick.AddListener(SkipButtonClicked);
            videoButton.onClick.AddListener(VideoButtonClicked);
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

            _currentIntValue = Mathf.CeilToInt(_collectedMoney * _coefficient);
            valueText.text = "+" + Utility.MinifyLong(_currentIntValue);
        }

        private void IncreasingFinished(bool isGloveReady)
        {
            if (isGloveReady)
                onGloveReady?.Invoke();
            else
                ShrinkGlove();
        }

        public void ShrinkGlove()
        {
            itemWidgetPanel.gameObject.SetActive(false);
            CheckGloveCondition();
        }

        private void CheckGloveCondition()
        {   
            OpenRewardPanel();
            Invoke("ShowSkipButton", 3f);
        }

        private void OpenRewardPanel()
        {
            var sq = DOTween.Sequence();
            sq.Append(rewardPanel.DOScale(Vector3.one * 1.2f, 0.5f));
            sq.Append(rewardPanel.DOScale(Vector3.one * 0.8f, 0.4f));
            sq.Append(rewardPanel.DOScale(Vector3.one, 0.3f));
        }

        private void SkipButtonClicked()
        {
            onLevelFinishedPanelClosed?.Invoke(0);
        }

        private void VideoButtonClicked()
        {
            AdManager.Instance.PrepareOnRVShownEvent(VideoShown);
            AdManager.Instance.ShowRewardedAd();
        }

        private void VideoShown()
        {
            onLevelFinishedPanelClosed?.Invoke(_currentIntValue);
        }

        private void ShowSkipButton()
        {
            skipButton.gameObject.SetActive(true);
        }
    }
}