using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MagazineData", menuName = "Inex/Data/MagazineData", order = 0)]
    public class MagazineData : ScriptableObject
    {
        public int MaxMagazineLevel;
        public List<MagazineModel> MagazineModels;
        public List<int> LevelsToShowPanel;
    }
}