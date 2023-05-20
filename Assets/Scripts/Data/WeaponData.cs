using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Inex/Data/WeaponData", order = 0)]

    public class WeaponData : ScriptableObject
    {
        public List<WeaponModel> WeaponModels;
    }
}