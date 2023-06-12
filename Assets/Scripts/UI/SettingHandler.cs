using System;
using DG.Tweening;
using Managers;
using Statics;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingHandler : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private Button soundBtn;
        [SerializeField] private Image soundImage;
        [SerializeField] private Sprite muteSprite;
        [SerializeField] private Sprite notMuteSprite;
        [SerializeField] private Transform backgroundTransform;
        [SerializeField] private Transform backgroundCloseSpot;
        [SerializeField] private Transform backgroundOpenSpot;

        private bool _isOpen;
        private bool _isMute;
        private void Start()
        {
            btn.onClick.AddListener(ButtonClicked);
            soundBtn.onClick.AddListener(SoundButtonClicked);
            _isMute = Prefs.Mute;
            soundImage.sprite = _isMute ? muteSprite : notMuteSprite;
        }

        private void ButtonClicked()
        {
            _isOpen = !_isOpen;
            if (_isOpen)
            {
                var targetRotation = new Vector3(0, 0, 90);
                btn.transform.DOLocalRotate(targetRotation, 0.5f);
            }
            else
            {
                btn.transform.DOLocalRotate(Vector3.zero, 0.5f);
            }
            backgroundTransform.DOMove(_isOpen ? backgroundOpenSpot.position : backgroundCloseSpot.position, 0.5f);
        }

        private void SoundButtonClicked()
        {
            _isMute = !_isMute;
            Prefs.Mute = _isMute;
            SoundManager.Instance.MuteHandling(_isMute);
            soundImage.sprite = _isMute ? muteSprite : notMuteSprite;
        }
    }
}