using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class GloveModel
    {
        public GameObject GloveRightPrefab;
        public GameObject GloveLeftPrefab;
        public int Level;
        public float FireRate;
        public float Power;
        public float Range;
        public Sprite MainSprite;
        public Sprite ShadowSprite;
    }
}