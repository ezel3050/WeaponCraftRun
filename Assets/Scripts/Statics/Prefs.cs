using UnityEngine;

namespace Statics
{
    public class Prefs
    {
        public static int Money
        {
            get => PlayerPrefs.GetInt("Money", 10000);
            set => PlayerPrefs.SetInt("Money", value);
        }
        
        public static float WeaponLevel
        {
            get => PlayerPrefs.GetFloat("WeaponLevel", 1800);
            set => PlayerPrefs.SetFloat("WeaponLevel", value);
        }
    }
}