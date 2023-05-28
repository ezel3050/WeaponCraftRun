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
            var properLevel = weaponYear - (weaponYear % 10);
            var properModel = weaponData.WeaponModels.Find(model => Math.Abs(model.Year - properLevel) < 0.1f);
            return properModel;
        }

        public List<WeaponModel> GetTwoSideModel(int year)
        {
            var modelList = new List<WeaponModel>();
            var properLevel = year - (year % 10);
            var properModel = weaponData.WeaponModels.Find(model => Math.Abs(model.Year - properLevel) < 0.1f);
            modelList.Add(properModel);
            var nextProperModel = weaponData.WeaponModels.Find(model => Math.Abs(model.Year - (properLevel + 10)) < 0.1f);
            modelList.Add(nextProperModel);
            return modelList;
        }
    }
}