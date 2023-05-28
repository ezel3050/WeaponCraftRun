using System;
using Statics;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private UIWeaponProgress uiWeaponProgress;

        public static UIManager Instance;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
        
        private void Start()
        {
            CurrencyHandler.onValueChanged += CurrencyChanged;
        }

        public void SetLevelText(int level)
        {
            if (level == 0)
                levelText.text = "Tutorial";
            else
                levelText.text = "Level " + level;
        }

        private void CurrencyChanged(int value)
        {
            moneyText.text = "$" + value;
        }

        public void SyncWeaponUIProgress(int year)
        {
            uiWeaponProgress.Initialize(year);
        }
    }
}