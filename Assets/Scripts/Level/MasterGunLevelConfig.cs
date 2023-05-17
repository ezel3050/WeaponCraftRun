using DefaultNamespace.Core;
using UnityEngine;

namespace DefaultNamespace.Level
{
    [CreateAssetMenu(fileName = "MasterGunLevelConfig", menuName = "Inex/GameManager/MasterGun/MasterGunLevelConfig", order = 0)]

    public class MasterGunLevelConfig : BaseLevelConfig
    {
        public override string SceneName { get; }
    }
}