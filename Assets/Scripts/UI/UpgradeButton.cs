using System;
using Enums;
using Managers;
using Statics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private UpgradeType type;
        [SerializeField] private GameObject moneyPanel;
        [SerializeField] private GameObject videoPanel;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button btn;

        public Action<UpgradeType> onLevelChanged;

        private int _level;
        private bool _isVideoMode;
        private SoundManager _soundManager;
        
        private void Start()
        {
            GetLevel();
            CheckButtonCondition(CurrencyHandler.CurrentMoney);
            btn.onClick.AddListener(ButtonClicked);
            titleText.text = type.ToString();
            levelText.text = "Level " + _level;
            priceText.text = Utility.MinifyLong(_level * 100) + "$";
            CurrencyHandler.onValueChanged += ValueChanged;
            _soundManager = SoundManager.Instance;
        }

        private void ValueChanged(int arg1, Vector3 arg2, bool arg3)
        {
            CheckButtonCondition(CurrencyHandler.CurrentMoney);
        }

        private void ButtonClicked()
        {
            if (_isVideoMode)
            {
                AdManager.Instance.PrepareOnRVShownEvent(VideoShown);
                AdManager.Instance.ShowRewardedAd();
            }
            else
            {
                CurrencyHandler.DecreaseMoney(_level * 100);
                ApplyChanges();
            }
        }

        private void VideoShown()
        {
            ApplyChanges();
        }

        private void ApplyChanges()
        {
            SetLevel();
            Sync();
            _soundManager.UpgradeButton();
        }

        public void Sync()
        {
            GetLevel();
            CheckButtonCondition(CurrencyHandler.CurrentMoney);
        }

        private void GetLevel()
        {
            _level = type switch
            {
                UpgradeType.FireRate => Prefs.FireRateLevel,
                UpgradeType.InitYear => Prefs.WeaponLevel - 1799,
                UpgradeType.Range => Prefs.FireRangeLevel,
                UpgradeType.Income => Prefs.IncomeLevel,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private void SetLevel()
        {
            switch (type)
            {
                case UpgradeType.FireRate:
                    Prefs.FireRateLevel++;
                    break;
                case UpgradeType.InitYear:
                    Prefs.WeaponLevel++;
                    break;
                case UpgradeType.Range:
                    Prefs.FireRangeLevel++;
                    break;
                case UpgradeType.Income:
                    Prefs.IncomeLevel++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            onLevelChanged?.Invoke(type);
        }

        private void CheckButtonCondition(int value)
        {
            levelText.text = "Level " + _level;
            priceText.text = _level * 100 + "$";
            var buttonValue = _level * 100;
            if (buttonValue > value)
            {
                moneyPanel.SetActive(false);
                videoPanel.SetActive(true);
                _isVideoMode = true;
            }
            else
            {
                moneyPanel.SetActive(true);
                videoPanel.SetActive(false);
                _isVideoMode = false;
            }
        }
    }
}