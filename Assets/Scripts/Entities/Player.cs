using System;
using DefaultNamespace.Components;
using DefaultNamespace.Entities;
using DG.Tweening;
using Enums;
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
        [SerializeField] private Transform body;

        private WeaponModel _weaponModel;
        private Weapon _cloneWeapon;
        private float _fireRate;
        private float _fireRange;
        private int _power;
        public void Initialize()
        {
            FillVariables();
            CreateWeapon();
        }

        private void FillVariables()
        {
            var model  = ContentManager.Instance.GetProperWeaponModel(Prefs.WeaponLevel);
            FillModel(model);
        }

        private void FillModel(WeaponModel model)
        {
            _weaponModel = new WeaponModel()
            {
                Year = model.Year,
                Weapon = model.Weapon,
                Bullet = model.Bullet,
                Rate = model.Rate,
                Range = model.Range,
                Power = model.Power,
                OuterBulletMaterial = model.OuterBulletMaterial,
                InnerBulletMaterial = model.InnerBulletMaterial
            };
        }

        private void WeaponTriggerEnter(Collider obj)
        {
            if (obj.CompareTag("Gate"))
                GatePassed(obj);
            if (obj.CompareTag("BulletGate"))
                BulletGatePassed(obj);
        }

        private void GatePassed(Collider obj)
        {
            var gate = obj.GetComponent<Gate>();

            HandleGateEntry(gate.GateTypes, gate.CurrentValue);
            
            gate.DestroyItself();
        }
        
        private void BulletGatePassed(Collider obj)
        {
            var gate = obj.GetComponent<BulletGate>();

            HandleGateEntry(GateTypes.Year, gate.GetValue());
            
            gate.PassedFromGate();
        }

        private void HandleGateEntry(GateTypes gateType, float gateValue)
        {
            switch (gateType)
            {
                case GateTypes.Year:
                    YearChanged(gateValue);
                    break;
                case GateTypes.Month:
                    YearChanged(Utility.ConvertMonthIntoYear(gateValue));
                    break;
                case GateTypes.FireRate:
                    FireRateChanged(gateValue);
                    break;
                case GateTypes.FireRange:
                    FireRangeChanged(gateValue);
                    break;
                case GateTypes.Money:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void YearChanged(float value)
        {
            _weaponModel.Year += value;
            CheckIfIsThereNewGunAvailable();
        }

        private void CheckIfIsThereNewGunAvailable()
        {
            var currentYear = _weaponModel.Year;
            var newModel = ContentManager.Instance.GetProperWeaponModel(currentYear);
            if (newModel.Weapon == _weaponModel.Weapon) return;
            FillModel(newModel);
            _weaponModel.Year = currentYear;
            RotationAction();
            CreateWeapon();
        }

        private void RotationAction()
        {
            var rotation = new Vector3(0, 360, 0);
            body.DOLocalRotate(rotation, 0.3f, RotateMode.FastBeyond360);
        }

        private void CreateWeapon()
        {
            DestroyCurrentWeapon();
            _cloneWeapon = Instantiate(_weaponModel.Weapon, weaponSpot);
            _cloneWeapon.transform.localPosition = Vector3.zero;
            ApplyChangesOnModel();
            _cloneWeapon.Initialize(_weaponModel);
            _cloneWeapon.ShootActivateHandler(true);
            _cloneWeapon.onTriggerEnter += WeaponTriggerEnter;
        }

        private void ApplyChangesOnModel()
        {
            _weaponModel.Range += _fireRange;
            _weaponModel.Rate += _fireRate;
            _weaponModel.Power += _power;
        }

        private void DestroyCurrentWeapon()
        {
            if (_cloneWeapon)
            {
                _cloneWeapon.ShootActivateHandler(false);
                Destroy(_cloneWeapon.gameObject);
            }
        }

        private void FireRateChanged(float value)
        {
            var fixedValue = value / 20;
            _fireRate += fixedValue;
            _weaponModel.Rate += _fireRate;
            _cloneWeapon.Initialize(_weaponModel);
        }

        private void FireRangeChanged(float value)
        {
            var fixedValue = value / 100;
            _fireRange += fixedValue;
            _weaponModel.Range += _fireRange;
            _cloneWeapon.Initialize(_weaponModel);
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