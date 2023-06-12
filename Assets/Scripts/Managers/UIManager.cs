using System;
using System.Collections.Generic;
using Components;
using DefaultNamespace;
using Enums;
using Level;
using Statics;
using TMPro;
using UI;
using Unity.VisualScripting;
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
        [SerializeField] private DualWeaponButton dualWeaponButton;
        [SerializeField] private FadingText fadingTextPrefab;
        [SerializeField] private UnlockItemPanel unlockItemPanelPrefab;
        [SerializeField] private CannonPurchasePanel cannonPurchasePanelPrefab;
        [SerializeField] private ClaimPanel claimPanelPrefab;
        [SerializeField] private WeaponOfferPanel weaponOfferPanelPrefab;
        [SerializeField] private MagazinePanel magazinePanelPrefab;
        [SerializeField] private DualShootPanel dualShootPanelPrefab;
        [SerializeField] private Transform parentSpot;

        private LevelFinishedPanel _levelFinishedPanel;
        private UnlockItemPanel _unlockItemPanel;
        private CannonPurchasePanel _cannonPurchasePanel;
        private ClaimPanel _claimPanel;
        private WeaponOfferPanel _weaponOfferPanel;
        private MagazinePanel _magazinePanel;
        private DualShootPanel _dualShootPanel;
        private bool _isShowingCannonPurchasePanelOnNextLevel;

        public Action onCannonPurchased;
        public Action onWeaponUpgraded;

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
            _levelFinishedPanel.onLevelFinishedPanelClosed += LevelFinishedPanelClosed;
            dualWeaponButton.onDualWeaponClicked += DualWeaponClicked;
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

        private void DualWeaponClicked()
        {
            ActiveDualGunButton(false);
            var currentLevel = (MasterGunLevel)LevelManager.CurrentLevel;
            currentLevel.ActiveSecondGun();
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

        private void LevelFinishedPanelClosed(int value)
        {
            CurrencyHandler.IncreaseMoney(value, Vector3.zero, false);
            ResetPanels();
        }

        private void ResetPanels()
        {
            Destroy(_levelFinishedPanel.gameObject);
            onCannonPurchased = null;
            onWeaponUpgraded = null;
            _levelFinishedPanel = Instantiate(levelFinishedPanelPrefab, parentSpot);
            _levelFinishedPanel.onLevelFinishedPanelClosed += LevelFinishedPanelClosed;
            _levelFinishedPanel.onGloveReady += OpenGloveReadyPanel;
            _levelFinishedPanel.gameObject.SetActive(false);
            uiWeaponProgress.gameObject.SetActive(true);
            tapToStartPanel.gameObject.SetActive(true);
            if (Prefs.CanShowDualGunButton)
                ActiveDualGunButton(true);
            GameManager.InitializeLevelManager();
        }

        public void ActiveDualGunButton(bool isActive)
        {
            dualWeaponButton.gameObject.SetActive(isActive);
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

        private void UnlockPanelClosed(bool isAdSeen, Sprite icon)
        {
            if (isAdSeen)
            {
                _claimPanel = Instantiate(claimPanelPrefab, parentSpot);
                _claimPanel.Initialize(icon, () =>
                {
                    Prefs.GloveLevel++;
                    Prefs.ItemWidgetLevel = 0;
                    Destroy(_claimPanel.gameObject);
                });
                _levelFinishedPanel.ShrinkGlove();
                Destroy(_unlockItemPanel.gameObject);
            }
            else
            {
                Destroy(_unlockItemPanel.gameObject);
                _levelFinishedPanel.ShrinkGlove();
            }
        }

        public void OpenEndGameWeaponPanel(Sprite icon, Action callback)
        {
            _claimPanel = Instantiate(claimPanelPrefab, parentSpot);
            _claimPanel.Initialize(icon, () =>
            {
                Prefs.EndGameWeaponLevel++;
                Prefs.IsReachedEndGamePlatform = true;
                callback?.Invoke();
                Destroy(_claimPanel.gameObject);
            });
        }

        private void CreateCannonPurchasePanel()
        {
            _isShowingCannonPurchasePanelOnNextLevel = false;
            _cannonPurchasePanel = Instantiate(cannonPurchasePanelPrefab, parentSpot);
            _cannonPurchasePanel.onPanelClosed += CloseCannonPurchasePanel;
        }

        private void CloseCannonPurchasePanel(bool isAdSeen, Sprite icon)
        {
            if (isAdSeen)
            {
                _claimPanel = Instantiate(claimPanelPrefab, parentSpot);
                _claimPanel.Initialize(icon, () =>
                {
                    Prefs.CannonLevel++;
                    onCannonPurchased?.Invoke();
                    Destroy(_claimPanel.gameObject);
                });
                Destroy(_cannonPurchasePanel.gameObject);
            }
            else
                Destroy(_cannonPurchasePanel.gameObject);
        }

        public void AddCannonPanelOnQueueList()
        {
            _isShowingCannonPurchasePanelOnNextLevel = true;
        }

        public void ShowQueuePanel()
        {
            if (_isShowingCannonPurchasePanelOnNextLevel)
                CreateCannonPurchasePanel();
        }

        public void ShowWeaponOfferPanel()
        {
            _weaponOfferPanel = Instantiate(weaponOfferPanelPrefab, parentSpot);
            _weaponOfferPanel.onWeaponOfferPanelClosed += WeaponOfferPanelClosed;
        }

        private void WeaponOfferPanelClosed(bool isAdSeen)
        {
            if (isAdSeen)
            {
                Destroy(_weaponOfferPanel.gameObject);
                Prefs.WeaponLevel += 10;
                foreach (var upgradeButton in upgradeButtons)
                {
                    upgradeButton.Sync();
                }
                onWeaponUpgraded?.Invoke();
            }
            else
            {
                Destroy(_weaponOfferPanel.gameObject);
            }
        }

        public void CreateMagazinePanel()
        {
            _magazinePanel = Instantiate(magazinePanelPrefab, parentSpot);
            _magazinePanel.onPanelClosed += MagazinePanelClosed;
        }

        private void MagazinePanelClosed(bool isAdSeen)
        {
            if (isAdSeen)
            {
                Prefs.MagazineLevel++;
                var currentLevel = (MasterGunLevel)LevelManager.CurrentLevel;
                currentLevel.SyncMagazines();
                Destroy(_magazinePanel.gameObject);
            }
            else
            {
                Destroy(_magazinePanel.gameObject);
            }
        }

        public void CreateDualShootingPanel()
        {
            _dualShootPanel = Instantiate(dualShootPanelPrefab, parentSpot);
            _dualShootPanel.onPanelClosed += DualShootingPanelClosed;
        }

        private void DualShootingPanelClosed(bool isAdSeen)
        {
            if (isAdSeen)
            {
                DualWeaponClicked();
                Destroy(_dualShootPanel.gameObject);
            }
            else
            {
                Destroy(_dualShootPanel.gameObject);
            }
        }
    }
}