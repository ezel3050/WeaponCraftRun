using System;
using System.Collections.Generic;
using Components;
using Enums;
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
        [SerializeField] private List<UpgradeButton> upgradeButtons;
        [SerializeField] private RangeAndIncomePanel rangeAndIncomePanel;
        [SerializeField] private LevelFinishedPanel levelFinishedPanelPrefab;
        [SerializeField] private TapToStartPanel tapToStartPanel;
        [SerializeField] private FadingText fadingTextPrefab;
        [SerializeField] private UnlockItemPanel unlockItemPanelPrefab;
        [SerializeField] private Transform parentSpot;

        private LevelFinishedPanel _levelFinishedPanel;
        private UnlockItemPanel _unlockItemPanel;

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
            CurrencyChanged(CurrencyHandler.CurrentMoney, Vector3.zero, false);
            rangeAndIncomePanel.onRangeAndIncomePanelClosed += OpenLevelFinishedPanel;
            if (_levelFinishedPanel) Destroy(_levelFinishedPanel.gameObject);
            _levelFinishedPanel = Instantiate(levelFinishedPanelPrefab, parentSpot);
            _levelFinishedPanel.onLevelFinishedPanelClosed += ResetPanels;
            _levelFinishedPanel.onGloveReady += OpenGloveReadyPanel;
            _levelFinishedPanel.gameObject.SetActive(false);
        }

        public void OpenFinishingPanel()
        {
            rangeAndIncomePanel.gameObject.SetActive(true);
        }

        private void OpenLevelFinishedPanel()
        {
            rangeAndIncomePanel.gameObject.SetActive(false);
            _levelFinishedPanel.gameObject.SetActive(true);
        }

        public void SetLevelText(int level)
        {
            if (level == 0)
                levelText.text = "Tutorial";
            else
                levelText.text = "Level " + level;
        }

        private void CurrencyChanged(int value, Vector3 position, bool haveFadingText)
        {
            moneyText.text = "$" + Utility.MinifyLong(CurrencyHandler.CurrentMoney);
            if (!haveFadingText) return;
            CreateFadingText(value, position, true);
        }

        public void SyncWeaponUIProgress(int year, bool isInitSync)
        {
            uiWeaponProgress.Initialize(year, isInitSync);
        }

        public void SetUpgradeButtonsAction(Action<UpgradeType> callback)
        {
            foreach (var upgradeButton in upgradeButtons)
            {
                upgradeButton.onLevelChanged = null;
                upgradeButton.onLevelChanged = callback;
            }
        }

        private void ResetPanels()
        {
            Destroy(_levelFinishedPanel.gameObject);
            _levelFinishedPanel = Instantiate(levelFinishedPanelPrefab, parentSpot);
            _levelFinishedPanel.onLevelFinishedPanelClosed += ResetPanels;
            _levelFinishedPanel.onGloveReady += OpenGloveReadyPanel;
            _levelFinishedPanel.gameObject.SetActive(false);
            uiWeaponProgress.gameObject.SetActive(true);
            tapToStartPanel.gameObject.SetActive(true);
            GameManager.InitializeLevelManager();
        }

        public void DeActiveWeaponProgressUI()
        {
            uiWeaponProgress.gameObject.SetActive(false);
        }

        public void CreateFadingText(float value, Vector3 position, bool isMoney)
        {
            var fadingTextClone = Instantiate(fadingTextPrefab, position, Quaternion.identity, transform);
            fadingTextClone.Initialize(value, isMoney);
        }

        private void OpenGloveReadyPanel()
        {
            _unlockItemPanel = Instantiate(unlockItemPanelPrefab, parentSpot);
            _unlockItemPanel.onUnlockPanelClosed += UnlockPanelClosed;
        }

        private void UnlockPanelClosed(bool isAdSeen)
        {
            if (isAdSeen)
            {
                Destroy(_unlockItemPanel.gameObject);
                Prefs.GloveLevel++;
                Prefs.ItemWidgetLevel = 0;
            }
            else
            {
                Destroy(_unlockItemPanel.gameObject);
                _levelFinishedPanel.ShrinkGlove();
            }
        }
    }
}