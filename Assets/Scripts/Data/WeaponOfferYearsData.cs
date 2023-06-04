using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WeaponOfferYearsData", menuName = "Inex/Data/WeaponOfferYearsData", order = 0)]
    public class WeaponOfferYearsData : ScriptableObject
    {
        public List<int> WeaponOfferLevels;
    }
}