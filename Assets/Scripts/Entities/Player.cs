using System;
using DefaultNamespace.Components;
using DefaultNamespace.Entities;
using DG.Tweening;
using Enums;
using Managers;
using Models;
using Statics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private Transform weaponRightSpot;
        [SerializeField] private Transform weaponLeftSpot;
        [SerializeField] private Transform gloveSpot;
        [SerializeField] private Transform bodyRight;
        [SerializeField] private Transform bodyLeft;

        private WeaponModel _weaponModel;
        private Weapon _cloneWeapon;
        private Weapon _cloneSecondWeapon;
        private float _fireRate;
        private float _fireRange;
        private int _power;
        private bool _isTwoHandModeOn;
        private bool _isDied;

        public Action onPlayerDied;
        
        public void Initialize()
        {
            FillVariables();
            CreateWeapon();
        }

        private void FillVariables()
        {
            var currentYear = Prefs.WeaponLevel;
            var model  = ContentManager.Instance.GetProperWeaponModel(currentYear);
            FillModel(model);
            _weaponModel.Year = currentYear;
            UIManager.Instance.SyncWeaponUIProgress(_weaponModel.Year);
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
            if (obj.CompareTag("EndingBlock"))
            {
                if (_isDied) return;
                _isDied = true;
                movement.FullStop(true);
                transform.DOMoveZ(transform.localPosition.z - 2, 0.3f).onComplete = () =>
                {
                    movement.SyncZPos();
                    ApplyDeath();
                };
            }
        }

        private void ActiveTwoGun()
        {
            _isTwoHandModeOn = true;
            bodyRight.DOLocalMoveX(0.5f, 0.2f);
            bodyLeft.gameObject.SetActive(true);
            bodyLeft.DOLocalMoveX(-0.5f, 0);
            CreateSecondWeapon();
        }

        private void ApplyDeath()
        {
            _cloneWeapon.ShootActivateHandler(false);
            var firstTargetRotation = new Vector3(0, 0, -90);
            bodyRight.DOLocalRotate(firstTargetRotation, 0.5f).onComplete = () => onPlayerDied?.Invoke();
            if (_isTwoHandModeOn)
            {
                var secondTargetRotation = new Vector3(0, 0, 90);
                _cloneSecondWeapon.ShootActivateHandler(false);
                bodyLeft.DOLocalRotate(secondTargetRotation, 0.5f).onComplete = () => onPlayerDied?.Invoke();
                bodyLeft.DOLocalMoveX(-1.5f, 0.3f);
                bodyRight.DOLocalMoveX(1.5f, 0.3f);
            }
        }

        private void WeaponHoleTriggerEnter(Collider obj)
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
                case GateTypes.DualGun:
                    ActiveTwoGun();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void YearChanged(float value)
        {
            _weaponModel.Year += Mathf.CeilToInt(value);
            UIManager.Instance.SyncWeaponUIProgress(_weaponModel.Year);
            CheckIfIsThereNewGunAvailable();
        }

        private void CheckIfIsThereNewGunAvailable()
        {
            var currentYear = _weaponModel.Year;
            var newModel = ContentManager.Instance.GetProperWeaponModel(currentYear);
            if (newModel.Weapon == _weaponModel.Weapon) return;
            FillModel(newModel);
            _weaponModel.Year = currentYear;
            ApplyChangesOnModel();
            RotationAction();
            CreateWeapon();
            if (_isTwoHandModeOn)
                CreateSecondWeapon();
        }

        private void RotationAction()
        {
            var rotation = new Vector3(0, 360, 0);
            bodyRight.DOLocalRotate(rotation, 0.3f, RotateMode.FastBeyond360);
            bodyLeft.DOLocalRotate(rotation, 0.3f, RotateMode.FastBeyond360);
        }

        private void CreateWeapon()
        {
            DestroyCurrentWeapon();
            _cloneWeapon = Instantiate(_weaponModel.Weapon, weaponRightSpot);
            _cloneWeapon.transform.localPosition = Vector3.zero;
            _cloneWeapon.Initialize(_weaponModel);
            _cloneWeapon.ShootActivateHandler(true);
            _cloneWeapon.onWeaponHoleTriggerEnter += WeaponHoleTriggerEnter;
            _cloneWeapon.onWeaponTriggerEnter += WeaponTriggerEnter;
        }
        
        private void CreateSecondWeapon()
        {
            DestroySecondWeapon();
            _cloneSecondWeapon = Instantiate(_weaponModel.Weapon, weaponLeftSpot);
            _cloneSecondWeapon.transform.localPosition = Vector3.zero;
            _cloneSecondWeapon.Initialize(_weaponModel);
            _cloneSecondWeapon.ShootActivateHandler(true);
            // _cloneSecondWeapon.onWeaponHoleTriggerEnter += WeaponHoleTriggerEnter;
            // _cloneSecondWeapon.onWeaponTriggerEnter += WeaponTriggerEnter;
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
        
        private void DestroySecondWeapon()
        {
            if (_cloneSecondWeapon)
            {
                _cloneSecondWeapon.ShootActivateHandler(false);
                Destroy(_cloneSecondWeapon.gameObject);
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