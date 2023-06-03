using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EndWeaponData", menuName = "Inex/Data/EndWeaponData", order = 0)]

    public class EndWeaponData : ScriptableObject
    {
        public int MaxLevel;
        public List<EndWeaponModel> EndWeaponModels;
    }
}