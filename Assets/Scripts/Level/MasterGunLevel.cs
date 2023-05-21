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

        public override void InitializeLevel(BaseLevelConfig config)
        {
            base.InitializeLevel(config);
            playerController.Initialize();
            SetPlayerToInputManager();
            foreach (var magazineHandler in magazineHandlers)
            {
                magazineHandler.onMagazineGotFull += OneMagazineGotFull;
            }
        }

        protected internal override void StartLevel()
        {
            base.StartLevel();
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

        protected internal override bool IsWon()
        {
            return false;
        }

        protected internal override void PrepareUIRequirementsabstract()
        {
            
        }
    }
}