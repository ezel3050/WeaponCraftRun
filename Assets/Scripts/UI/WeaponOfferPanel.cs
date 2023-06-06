using System;
using DefaultNamespace.Entities;
using Managers;
using Models;
using Statics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class WeaponOfferPanel : MonoBehaviour
    {
        [SerializeField] private Transform weaponSpot;
        [SerializeField] private Transform lightTransform;
        [SerializeField] private TextMeshProUGUI fireRateCurrentValueText;
        [SerializeField] private TextMeshProUGUI fireRateNextValueText;
        [SerializeField] private TextMeshProUGUI powerCurrentValueText;
        [SerializeField] private TextMeshProUGUI powerNextValueText;
        [SerializeField] private TextMeshProUGUI rangeCurrentValueText;
        [SerializeField] private TextMeshProUGUI rangeNextValueText;
        [SerializeField] private Button skipButton;
        [SerializeField] private Button videoButton;
        [SerializeField] private float lightRotationSpeed;
        [SerializeField] private float weaponRotationSpeed;

        private Weapon _weapon;
        private Vector3 _tempLightRotation;
        private Vector3 _tempWeaponRotation;
        private int _currentWeaponLevel;

        public Action<bool> onWeaponOfferPanelClosed;

        private void Start()
        {
            _tempLightRotation = Vector3.zero;
            _tempWeaponRotation = Vector3.zero;
            _currentWeaponLevel = Prefs.WeaponLevel;
            var models = ContentManager.Instance.GetTwoSideModel(_currentWeaponLevel);
            _weapon = Instantiate(models[1].Weapon, weaponSpot);
            _weapon.transform.localPosition = Vector3.zero;
            SetValues(models[0], models[1]);
            skipButton.onClick.AddListener(SkipButtonClicked);
            videoButton.onClick.AddListener(VideoButtonClicked);
            Invoke("ShowSkipButton", 3f);

        }

        private void SetValues(WeaponModel currentModel, WeaponModel nextModel)
        {
            fireRateCurrentValueText.text = "=+" + currentModel.Rate;
            fireRateNextValueText.text = "+" + nextModel.Rate;
            powerCurrentValueText.text = "=+" + currentModel.Power;
            powerNextValueText.text = "+" + nextModel.Power;
            rangeCurrentValueText.text = "=+" + currentModel.Range;
            rangeNextValueText.text = "+" + nextModel.Range;
            if (nextModel.Rate > currentModel.Rate)
                fireRateNextValueText.color = Color.green;
            if (nextModel.Power > currentModel.Power)
                powerNextValueText.color = Color.green;
            if (nextModel.Range > currentModel.Range)
                rangeNextValueText.color = Color.green;
        }
        
        private void Update()
        {
            _tempLightRotation.z += Time.deltaTime * -lightRotationSpeed;
            lightTransform.rotation = Quaternion.Euler(_tempLightRotation);
            _tempWeaponRotation.y += Time.deltaTime * weaponRotationSpeed;
            _weapon.transform.localRotation = Quaternion.Euler(_tempWeaponRotation);
        }
        
        private void SkipButtonClicked()
        {
            onWeaponOfferPanelClosed?.Invoke(false);
        }

        private void ShowSkipButton()
        {
            skipButton.gameObject.SetActive(true);
        }

        private void VideoButtonClicked()
        {
            AdManager.Instance.PrepareOnRVShownEvent(VideoShown);
            AdManager.Instance.ShowRewardedAd();
        }

        private void VideoShown()
        {
            onWeaponOfferPanelClosed?.Invoke(true);
        }
    }
}