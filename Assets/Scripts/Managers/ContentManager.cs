using System;
using System.Collections.Generic;
using Data;
using Models;
using UnityEngine;

namespace Managers
{
    public class ContentManager : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private EndWeaponData endWeaponData;
        [SerializeField] private GloveData gloveData;

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
            var nextProperModel = weaponData.WeaponModels.Find(model => Math.Abs(model.Year - (properLevel + 10)) < 0.1f);
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
    }
}