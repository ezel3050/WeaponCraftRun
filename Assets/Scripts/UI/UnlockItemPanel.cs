using System;
using Managers;
using Models;
using Statics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnlockItemPanel : MonoBehaviour
    {
        [SerializeField] private Transform lightTransform;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI fireRateCurrentValueText;
        [SerializeField] private TextMeshProUGUI fireRateNextValueText;
        [SerializeField] private TextMeshProUGUI powerCurrentValueText;
        [SerializeField] private TextMeshProUGUI powerNextValueText;
        [SerializeField] private TextMeshProUGUI rangeCurrentValueText;
        [SerializeField] private TextMeshProUGUI rangeNextValueText;
        [SerializeField] private Button skipButton;
        [SerializeField] private Button videoButton;
        [SerializeField] private float rotationSpeed;

        private Vector3 _tempRotation;
        private int _currentGloveLevel;

        public Action<bool, Sprite> onUnlockPanelClosed;

        private void Start()
        {
            _tempRotation = Vector3.zero;
            _currentGloveLevel = Prefs.GloveLevel;
            var currentLevelModel = ContentManager.Instance.GetGloveModel(_currentGloveLevel);
            var nextLevelModel = ContentManager.Instance.GetGloveModel(_currentGloveLevel + 1);
            icon.sprite = nextLevelModel.MainSprite;
            SetValues(currentLevelModel, nextLevelModel);
            skipButton.onClick.AddListener(SkipButtonClicked);
            videoButton.onClick.AddListener(VideoButtonClicked);
            Invoke("ShowSkipButton", 3f);
        }

        private void SetValues(GloveModel currentLevelModel, GloveModel nextLevelModel)
        {
            fireRateCurrentValueText.text = ":+" + currentLevelModel.FireRate;
            fireRateNextValueText.text = "+" + nextLevelModel.FireRate;
            powerCurrentValueText.text = ":+" + currentLevelModel.Power;
            powerNextValueText.text = "+" + nextLevelModel.Power;
            rangeCurrentValueText.text = ":+" + currentLevelModel.Range;
            rangeNextValueText.text = "+" + nextLevelModel.Range;
            if (nextLevelModel.FireRate > currentLevelModel.FireRate)
                fireRateNextValueText.color = Color.green;
            if (nextLevelModel.Power > currentLevelModel.Power)
                powerNextValueText.color = Color.green;
            if (nextLevelModel.Range > currentLevelModel.Range)
                rangeNextValueText.color = Color.green;
        }

        private void Update()
        {
            _tempRotation.z += Time.deltaTime * -rotationSpeed;
            lightTransform.rotation = Quaternion.Euler(_tempRotation);
        }

        private void SkipButtonClicked()
        {
            onUnlockPanelClosed?.Invoke(false,icon.sprite);
        }

        private void VideoButtonClicked()
        {
            AdManager.Instance.PrepareOnRVShownEvent(VideoShown);
            AdManager.Instance.ShowRewardedAd();
        }

        private void VideoShown()
        {
            onUnlockPanelClosed?.Invoke(true,icon.sprite);
        }

        private void ShowSkipButton()
        {
            skipButton.gameObject.SetActive(true);
        }
    }
}