using System.Collections.Generic;
using Components;
using DefaultNamespace.Core;
using DG.Tweening;
using Entities;
using Managers;
using Statics;
using UnityEngine;

namespace Level
{
    public class MasterGunLevel : BaseLevel
    {
        [SerializeField] private Player playerController;
        [SerializeField] private List<MagazineHandler> magazineHandlers;
        [SerializeField] private GameObject rail;
        [SerializeField] private FinishLine finishLine;
        [SerializeField] private Ending ending;
        [SerializeField] private List<BulletGateSystem> bulletGateSystems;
        [SerializeField] private float bulletGateSystemsCoefficient;
        [SerializeField] private Material platformMaterial;
        [SerializeField] private float materialPercent;

        private bool _isFinishLinePassed;

        public override void InitializeLevel(BaseLevelConfig config)
        {
            base.InitializeLevel(config);
            UIManager.Instance.SetLevelText(GameManagementPlayerPrefs.PlayerLevel);
            UIManager.Instance.ShowQueuePanel();
            if (ContentManager.Instance.CanShowWeaponOfferPanel(GameManagementPlayerPrefs.PlayerLevel))
                UIManager.Instance.ShowWeaponOfferPanel();
            if (ContentManager.Instance.CanShowMagazinePanel(GameManagementPlayerPrefs.PlayerLevel))
                UIManager.Instance.CreateMagazinePanel();
            if (ContentManager.Instance.CanShowDualWeaponPanel(GameManagementPlayerPrefs.PlayerLevel))
                UIManager.Instance.CreateDualShootingPanel();
            playerController.Initialize();
            SetPlayerToInputManager();
            CurrencyHandler.ResetData();
            platformMaterial.DOTiling(new Vector2(1, 0.3f * materialPercent), 0f);
            foreach (var magazineHandler in magazineHandlers)
            {
                magazineHandler.onMagazineGotFull += OneMagazineGotFull;
            }
            CameraManager.Instance.SetCameraFollow(playerController.transform);
            CameraManager.Instance.TurnStartCameraOn();
            ending.CreateHighScore();
            foreach (var bulletGateSystem in bulletGateSystems)
            {
                bulletGateSystem.SetCoefficient(bulletGateSystemsCoefficient);
            }

            finishLine.onFinishLinePassed += PlayerPassedFinishLine;
            playerController.onPlayerDied += PlayerDied;
        }

        protected internal override void StartLevel()
        {
            base.StartLevel();
            playerController.LevelStarted();
            playerController.FullStop(false);
            playerController.ShootActivateHandler(true, true);
            playerController.ShootActivateHandler(true, false);
            playerController.CannonActiveHandler(true);
            CameraManager.Instance.TurnInGameCameraOn();
        }
        
        private void PlayerPassedFinishLine()
        {
            _isFinishLinePassed = true;
            playerController.DeActiveYearTag();
            UIManager.Instance.DeActiveWeaponProgressUI();
            CameraManager.Instance.TurnEndingCameraOn();
        }

        private void SetPlayerToInputManager()
        {
            InputManager.Instance.SetPlayer(playerController);
        }

        public void SyncMagazines()
        {
            foreach (var magazineHandler in magazineHandlers)
            {
                magazineHandler.Sync();
            }
        }

        public void ActiveSecondGun()
        {
            playerController.ActiveTwoGun();
        }

        protected override void SubscribeToLevelRelatedEvents()
        {
            
        }

        private void OneMagazineGotFull(MagazineHandler magazineHandler)
        {
            magazineHandler.JumpOnRail(rail.transform.localPosition.x);
        }

        protected override void UnSubscribeFromLevelRelatedEvents()
        {
            
        }

        protected internal override int CalculateEarnedMoney()
        {
            return 0;
        }

        protected internal override int CalculateScore()
        {
            return 0;
        }

        protected internal override float CalculateSatisfaction()
        {
            return 0;
        }
        
        private void PlayerDied()
        {
            FinishLevel();
        }

        protected internal override bool IsWon()
        {
            return _isFinishLinePassed;
        }

        protected internal override void PrepareUIRequirementsabstract()
        {
            
        }

        protected internal override void FinishLevel()
        {
            base.FinishLevel();
            UIManager.Instance.OpenFinishingPanel();
        }
    }
}