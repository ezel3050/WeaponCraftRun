using System;
using Managers;
using Statics;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CannonPurchasePanel : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Transform lightTransform;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Button videoBtn;
        [SerializeField] private Button skipBtn;

        private Vector3 _tempRotation;

        public Action<bool,Sprite> onPanelClosed;

        private void Start()
        {
            var level = Prefs.CannonLevel;
            var model = ContentManager.Instance.GetCannonModel(level + 1);
            iconImage.sprite = model.CannonSprite;
            _tempRotation = Vector3.zero;
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