using DefaultNamespace.Core;
using Managers;
using UnityEngine;

namespace DefaultNamespace.Level
{
    public class MasterGunLevel : BaseLevel
    {
        [SerializeField] private PlayerController playerController;

        public override void InitializeLevel(BaseLevelConfig config)
        {
            base.InitializeLevel(config);
            SetPlayerToInputManager();
        }
        
        private void SetPlayerToInputManager()
        {
            InputManager.Instance.SetPlayer(playerController);
        }

        protected override void SubscribeToLevelRelatedEvents()
        {
            
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