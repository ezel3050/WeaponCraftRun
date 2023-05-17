using UnityEngine;

namespace DefaultNamespace.Core
{
    public abstract class BaseLevelConfig : ScriptableObject
    {
        public abstract string SceneName { get; }
        public BaseLevel yourLevelPrefab;

    }
}