using System.Collections.Generic;
using Components;
using DefaultNamespace.Core;
using Entities;
using Managers;
using UnityEngine;

namespace Level
{
    public class MasterGunLevel : BaseLevel
    {
        [SerializeField] private Player playerController;
        [SerializeField] private List<MagazineHandler> magazineHandlers;
        [SerializeField] private GameObject rail;
        [SerializeField] private FinishLine finishLine;

        private bool _isFinishLinePassed;

        public override void InitializeLevel(BaseLevelConfig config)
        {
            base.InitializeLevel(config);
            UIManager.Instance.SetLevelText(GameManagementPlayerPrefs.PlayerLevel);
            playerController.Initialize();
            SetPlayerToInputManager();
            foreach (var magazineHandler in magazineHandlers)
            {
                magazineHandler.onMagazineGotFull += OneMagazineGotFull;
            }
            CameraManager.Instance.SetCameraFollow(playerController.transform);
            CameraManager.Instance.TurnStartCameraOn();

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
            CameraManager.Instance.TurnInGameCameraOn();
        }
        
        private void PlayerPassedFinishLine()
        {
            _isFinishLinePassed = true;
        }

        private void SetPlayerToInputManager()
        {
            InputManager.Instance.SetPlayer(playerController);
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