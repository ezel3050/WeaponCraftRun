using System;
using Managers;
using Statics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CannonPurchasePanel : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Transform lightTransform;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private GameObject differenceObject;
        [SerializeField] private TextMeshProUGUI currentValue;
        [SerializeField] private TextMeshProUGUI nextValue;
        [SerializeField] private Button videoBtn;
        [SerializeField] private Button skipBtn;

        private Vector3 _tempRotation;

        public Action<bool,Sprite> onPanelClosed;

        private void Start()
        {
            var level = Prefs.CannonLevel;
            var canShowDifference = level != 0;
            var nextModel = ContentManager.Instance.GetCannonModel(level + 1);
            iconImage.sprite = nextModel.CannonSprite;
            _tempRotation = Vector3.zero;
            if (canShowDifference)
            {
                differenceObject.SetActive(true);
                var currentModel = ContentManager.Instance.GetCannonModel(level);
                currentValue.text = "+" + currentModel.Cannon.fireRate;
                nextValue.text = "+" + nextModel.Cannon.fireRate;
            }
            Invoke("ShowSkipButton", 3f);
            skipBtn.onClick.AddListener(SkipBtnClicked);
            videoBtn.onClick.AddListener(VideoBtnClicked);
        }

        private void Update()
        {
            _tempRotation.z += Time.deltaTime * -rotationSpeed;
            lightTransform.rotation = Quaternion.Euler(_tempRotation);
        }

        private void ShowSkipButton()
        {
            skipBtn.gameObject.SetActive(true);
        }

        private void SkipBtnClicked()
        {
            onPanelClosed?.Invoke(false, iconImage.sprite);
        }

        private void VideoBtnClicked()
        {
            AdManager.Instance.PrepareOnRVShownEvent(VideoShown);
            AdManager.Instance.ShowRewardedAd();
        }

        private void VideoShown()
        {
            onPanelClosed?.Invoke(true, iconImage.sprite);
        }
    }
}