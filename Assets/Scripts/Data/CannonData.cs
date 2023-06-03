using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CannonModel", menuName = "Inex/Data/CannonModel", order = 0)]
    public class CannonData : ScriptableObject
    {
        public int MaxLevel;
        public List<CannonModel> CannonModels;
    }
}