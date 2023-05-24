using System;
using DefaultNamespace.Entities;
using Entities;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class WeaponModel
    {
        public float Year;
        public Weapon Weapon;
        public Bullet Bullet;
        public float Rate;
        public float Range;
        public int Power;
        public Material OuterBulletMaterial;
        public Material InnerBulletMaterial;
    }
}