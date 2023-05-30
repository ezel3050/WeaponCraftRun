using System;
using Enums;
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
        [SerializeField] private Sprite availableBackground;
        [SerializeField] private Sprite notAvailableBackground;
        [SerializeField] private Sprite availableIcon;
        [SerializeField] private Sprite notAvailableIcon;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Button btn;

        public Action<UpgradeType> onLevelChanged;

        private int _level;
        private void Start()
        {
            GetLevel();
            CheckButtonCondition(CurrencyHandler.CurrentMoney);
            btn.onClick.AddListener(ButtonClicked);
            titleText.text = type.ToString();
            levelText.text = "Level " + _level;
            priceText.text = _level * 100 + "$";
        }

        private void ButtonClicked()
        {
            CurrencyHandler.DecreaseMoney(_level * 100);
            SetLevel();
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
                btn.interactable = false;
                iconImage.sprite = notAvailableIcon;
                backgroundImage.sprite = notAvailableBackground;
            }
            else
            {
                btn.interactable = true;
                iconImage.sprite = availableIcon;
                backgroundImage.sprite = availableBackground;
            }
        }
    }
}