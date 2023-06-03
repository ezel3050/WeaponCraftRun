using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GloveData", menuName = "Inex/Data/GloveData", order = 0)]
    public class GloveData : ScriptableObject
    {
        public int MaxLevel;
        public List<GloveModel> GloveModels;
    }
}