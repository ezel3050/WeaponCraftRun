using System;
using DefaultNamespace.Entities;
using Entities;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class WeaponModel
    {
        public int Year;
        public Weapon Weapon;
        public Bullet Bullet;
        public float Rate;
        public float Range;
        public float Power;
        public Material OuterBulletMaterial;
        public Material InnerBulletMaterial;
        public Sprite WeaponSprite;
        public Sprite WeaponBlackSprite;
    }
}