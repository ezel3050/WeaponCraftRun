using System;
using DefaultNamespace.Entities;
using Entities;

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
        public float Power;
    }
}