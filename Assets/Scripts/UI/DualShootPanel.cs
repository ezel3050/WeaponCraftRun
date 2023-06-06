using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DualShootPanel : MonoBehaviour
    {
        [SerializeField] private Transform lightTransform;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Button videoBtn;
        [SerializeField] private Button skipBtn;
        
        private Vector3 _tempRotation;

        public Action<bool> onPanelClosed;
        
        private void Start()
        {
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
            onPanelClosed?.Invoke(false);
        }

        private void VideoBtnClicked()
        {
            AdManager.Instance.PrepareOnRVShownEvent(VideoShown);
            AdManager.Instance.ShowRewardedAd();
        }

        private void VideoShown()
        {
            onPanelClosed?.Invoke(true);
        }
    }
}