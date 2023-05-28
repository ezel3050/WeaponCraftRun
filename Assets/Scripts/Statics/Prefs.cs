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
        
        public static float Income
        {
            get => PlayerPrefs.GetFloat("Income", 1);
            set => PlayerPrefs.GetFloat("Income", value);
        }
        
        public static int MagazineLevel
        {
            get => PlayerPrefs.GetInt("MagazineLevel", 1);
            set => PlayerPrefs.SetInt("MagazineLevel", value);
        }
        
        public static int WeaponLevel
        {
            get => PlayerPrefs.GetInt("WeaponLevel", 1800);
            set => PlayerPrefs.SetInt("WeaponLevel", value);
        }
    }
}