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
        
        public static int IncomeLevel
        {
            get => PlayerPrefs.GetInt("IncomeLevel", 1);
            set => PlayerPrefs.SetInt("IncomeLevel", value);
        }
        
        public static int FireRateLevel
        {
            get => PlayerPrefs.GetInt("FireRateLevel", 1);
            set => PlayerPrefs.SetInt("FireRateLevel", value);
        }
        
        public static int FireRangeLevel
        {
            get => PlayerPrefs.GetInt("FireRangeLevel", 1);
            set => PlayerPrefs.SetInt("FireRangeLevel", value);
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
        
        public static int ItemWidgetLevel
        {
            get => PlayerPrefs.GetInt("ItemWidgetLevel", 0);
            set => PlayerPrefs.SetInt("ItemWidgetLevel", value);
        }
        
        public static int GloveLevel
        {
            get => PlayerPrefs.GetInt("GloveLevel", 1);
            set => PlayerPrefs.SetInt("GloveLevel", value);
        }
        
        public static int EndGameWeaponLevel
        {
            get => PlayerPrefs.GetInt("EndGameWeaponLevel", 1);
            set => PlayerPrefs.SetInt("EndGameWeaponLevel", value);
        }
        
        public static int HighScore
        {
            get => PlayerPrefs.GetInt("HighScore", 0);
            set => PlayerPrefs.SetInt("HighScore", value);
        }
    }
}