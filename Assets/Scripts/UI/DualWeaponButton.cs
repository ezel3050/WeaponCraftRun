using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DualWeaponButton : MonoBehaviour
    {
        [SerializeField] private Button videoBtn;

        public Action onDualWeaponClicked;
        private void Start()
        {
            videoBtn.onClick.AddListener(VideoButtonClicked);
        }

        private void VideoButtonClicked()
        {
            AdManager.Instance.PrepareOnRVShownEvent(VideoShown);
            AdManager.Instance.ShowRewardedAd();
        }

        private void VideoShown()
        {
            onDualWeaponClicked?.Invoke();
        }
    }
}