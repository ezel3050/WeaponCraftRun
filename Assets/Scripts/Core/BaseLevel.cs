using System;
using UnityEngine;

namespace DefaultNamespace.Core
{
    public abstract class BaseLevel : MonoBehaviour
    {
        internal Action OnStart;
        internal Action<float> OnSatisfactionChanged;
        internal Action<LevelData> OnFinish;

        protected abstract void SubscribeToLevelRelatedEvents();
        protected abstract void UnSubscribeFromLevelRelatedEvents();

        internal bool ForcedToFinish => _forcedFinishStatus != LevelFinishStatus.None;
        private LevelFinishStatus _forcedFinishStatus;
        public LevelFinishStatus ForcedFinishStatus => _forcedFinishStatus;
        protected BaseLevelConfig _levelConfig;
        internal BaseLevelConfig LevelConfig => _levelConfig;
        private int _levelNumber;
        public int LevelNumber => _levelNumber;

        public virtual void InitializeLevel(BaseLevelConfig config)
        {
            _levelNumber = LevelManager.PlayerLevel;
            _levelConfig = config;
            NameLevelGameObject();
            FillVariables();
            PrepareUIRequirementsabstract();
        }

        private void NameLevelGameObject()
        {
            // DEBUGGING PURPOSES
            gameObject.name = $"{GetType().Name}_{LevelManager.LastLoadedLevelRealIndex}";
        }

        protected virtual void FillVariables()
        {
        }

        protected internal virtual void StartLevel()
        {
            SubscribeToLevelRelatedEvents();
            OnStart?.Invoke();
        }

        protected internal virtual void FinishLevel()
        {
            UnSubscribeFromLevelRelatedEvents();
            OnFinish?.Invoke(LevelData.GenerateFromLevel(this));
        }

        protected void SatisfactionChanged()
        {
            OnSatisfactionChanged?.Invoke(Mathf.Clamp(CalculateSatisfaction(), 0, 1f));
        }

        protected internal abstract int CalculateEarnedMoney();

        protected internal abstract int CalculateScore();

        protected internal abstract float CalculateSatisfaction();

        protected internal abstract bool IsWon();

        protected internal abstract void PrepareUIRequirementsabstract();

        internal void ForceFinishLevel(LevelFinishStatus forceStatus = LevelFinishStatus.None)
        {
            _forcedFinishStatus = forceStatus;
            FinishLevel();
        }
    }

    public enum LevelFinishStatus
    {
        None,
        Win,
        Lose,
        Draw,
        Retry
    }


    public class LevelData
    {
        public int EarnedMoney;
        public LevelFinishStatus ForcedStatus;
        public float Satisfaction;
        public int Score;
        public int LevelNumber;
        public int HumanReadableLevelNumber => LevelNumber + 1;
        public bool WinStatus;
        public int Attempt;

        public static LevelData GenerateFromLevel(BaseLevel level)
        {
            var levelData = new LevelData();
            levelData.ForcedStatus = level.ForcedFinishStatus;
            levelData.LevelNumber = level.LevelNumber;
            levelData.Attempt = LevelManager.GetLevelAttempts(level.LevelNumber);
            if (levelData.ForcedStatus == LevelFinishStatus.Retry)
            {
                levelData.EarnedMoney = 0;
                levelData.Score = 0;
            }
            else
            {
                levelData.EarnedMoney = level.CalculateEarnedMoney();
                levelData.Score = level.CalculateScore();
            }

            switch (levelData.ForcedStatus)
            {
                case LevelFinishStatus.Win:
                {
                    levelData.WinStatus = true;
                    levelData.Satisfaction = 1f;
                    break;
                }
                case LevelFinishStatus.Lose:
                {
                    levelData.WinStatus = false;
                    levelData.Satisfaction = 0f;
                    break;
                }
                case LevelFinishStatus.Retry:
                {
                    levelData.WinStatus = false;
                    levelData.Satisfaction = 0f;
                    break;
                }
                case LevelFinishStatus.None:
                {
                    levelData.WinStatus = level.IsWon();
                    levelData.Satisfaction = level.CalculateSatisfaction();
                    break;
                }
            }

            return levelData;
        }

        public override string ToString()
        {
            var export =
                $"EarnedMoney: {EarnedMoney}, Score: {Score}, WinStatus: {WinStatus}, Satisfaction: {Satisfaction}, ForcedTo?: {ForcedStatus.ToString()}, LevelNumber {LevelNumber}";
            return export;
        }
    }
}