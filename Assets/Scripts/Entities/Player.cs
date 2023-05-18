using DefaultNamespace.Components;
using DefaultNamespace.Entities;
using Managers;
using Models;
using Statics;
using UnityEngine;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private Transform weaponSpot;
        [SerializeField] private Transform gloveSpot;

        private WeaponModel _weaponModel;
        private Weapon _cloneWeapon;
        public void Initialize()
        {
            FillVariables();
            CreateWeapon();
        }

        private void FillVariables()
        {
            _weaponModel = ContentManager.Instance.GetProperWeaponModel(Prefs.WeaponLevel);
        }
        
        private void CreateWeapon()
        {
            DestroyCurrentWeapon();
            _cloneWeapon = Instantiate(_weaponModel.Weapon, weaponSpot);
            _cloneWeapon.transform.localPosition = Vector3.zero;
            _cloneWeapon.Initialize(_weaponModel);
            _cloneWeapon.ShootActivateHandler(true);
        }

        private void DestroyCurrentWeapon()
        {
            if (_cloneWeapon)
            {
                _cloneWeapon.ShootActivateHandler(false);
                Destroy(_cloneWeapon.gameObject);
            }
        }

        public void SetDeltaPosition(float value)
        {
            movement.SetDeltaPosition(value);
        }

        public void CancelMovement()
        {
            movement.CancelMovement();
        }
    }
}