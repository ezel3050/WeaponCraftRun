using System;
using System.Collections.Generic;
using Data;
using Models;
using Statics;
using UnityEngine;

namespace Managers
{
    public class ContentManager : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private EndWeaponData endWeaponData;
        [SerializeField] private GloveData gloveData;
        [SerializeField] private CannonData cannonData;
        [SerializeField] private WeaponOfferYearsData weaponOfferYearsData;
        [SerializeField] private MagazineData magazineData;
        [SerializeField] private List<int> dualWeaponPanelLevels;

        public static ContentManager Instance;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public WeaponModel GetProperWeaponModel(float weaponYear)
        {
            if (weaponYear < weaponData.MinWeaponLevel)
                weaponYear = weaponData.MinWeaponLevel;
            if (weaponYear > weaponData.MaxWeaponLevel)
                weaponYear = weaponData.MaxWeaponLevel;
            
            var properLevel = weaponYear - (weaponYear % 10);
            var properModel = weaponData.WeaponModels.Find(model => Math.Abs(model.Year - properLevel) < 0.1f);
            return properModel;
        }

        public List<WeaponModel> GetTwoSideModel(int year)
        {
            if (year < weaponData.MinWeaponLevel)
                year = weaponData.MinWeaponLevel;
            if (year > weaponData.MaxWeaponLevel)
                year = weaponData.MaxWeaponLevel;
            
            var modelList = new List<WeaponModel>();
            var properLevel = year - (year % 10);
            var properModel = weaponData.WeaponModels.Find(model => Math.Abs(model.Year - properLevel) < 0.1f);
            modelList.Add(properModel);
            var nextYearTarget = (properLevel + 10) > weaponData.MaxWeaponLevel
                ? weaponData.MaxWeaponLevel
                : properLevel + 10;
            var nextProperModel = weaponData.WeaponModels.Find(model => Math.Abs(model.Year - (nextYearTarget)) < 0.1f);
            modelList.Add(nextProperModel);
            return modelList;
        }

        public WeaponModel GetEndingWeaponModel(int level)
        {
            if (level > endWeaponData.MaxLevel)
                level = endWeaponData.MaxLevel;
            
            var target = endWeaponData.EndWeaponModels.Find(model => model.Level == level);
            return target.Model;
        }

        public GloveModel GetGloveModel(int level)
        {
            if (level > gloveData.MaxLevel)
                level = gloveData.MaxLevel;
            
            var target = gloveData.GloveModels.Find(model => model.Level == level);
            return target;
        }

        public CannonModel GetCannonModel(int level)
        {
            if (level > cannonData.MaxLevel)
                level = cannonData.MaxLevel;
            
            var target = cannonData.CannonModels.Find(model => model.Level == level);
            return target;
        }

        public bool CanShowWeaponOfferPanel(int level)
        {
            var weaponLevel = Prefs.WeaponLevel;
            var extra = weaponLevel % 10;
            weaponLevel -= extra;
            if (weaponLevel == weaponData.MaxWeaponLevel)
                return false;
            var result = weaponOfferYearsData.WeaponOfferLevels.Contains(level);
            return result;
        }
        
        public List<MagazineModel> GetTwoSideMagazineModels(int level)
        {
            if (level > magazineData.MaxMagazineLevel)
                level = magazineData.MaxMagazineLevel;

            var modelList = new List<MagazineModel>();
            var properModel = magazineData.MagazineModels.Find(model => model.Level == level);
            modelList.Add(properModel);
            var nextProperModel = magazineData.MagazineModels.Find(model => model.Level == (level > magazineData.MaxMagazineLevel ? magazineData.MaxMagazineLevel : level + 1));
            modelList.Add(nextProperModel);
            return modelList;
        }
        
        public bool CanShowMagazinePanel(int level)
        {
            var magazineLevel = Prefs.MagazineLevel;
            if (magazineLevel == magazineData.MaxMagazineLevel) return false;
            var result = magazineData.LevelsToShowPanel.Contains(level);
            return result;
        }
        
        public bool CanShowDualWeaponPanel(int level)
        {
            var result = dualWeaponPanelLevels.Contains(level);
            return result;
        }
    }
}