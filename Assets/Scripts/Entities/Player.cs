using System;
using Components;
using DefaultNamespace.Components;
using DefaultNamespace.Entities;
using DG.Tweening;
using Enums;
using Managers;
using Models;
using Statics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private Transform weaponRightSpot;
        [SerializeField] private Transform weaponLeftSpot;
        [SerializeField] private Transform gloveRightSpot;
        [SerializeField] private Transform gloveLeftSpot;
        [SerializeField] private Transform bodyRight;
        [SerializeField] private Transform bodyLeft;
        [SerializeField] private Transform cannonSpot;
        [SerializeField] private TextMeshPro yearText;
        [SerializeField] private GameObject yearTagObject;
        [SerializeField] private TriggerInvoker objectsTriggerInvoker;
        [SerializeField] private TriggerInvoker playerTriggerInvoker;

        private WeaponModel _weaponModel;
        private WeaponModel _originalWeaponModel;
        private GloveModel _gloveModel;
        private CannonModel _cannonModel;
        private GameObject _rightGlove;
        private GameObject _leftGlove;
        private Weapon _cloneWeapon;
        private Weapon _cloneSecondWeapon;
        private Cannon _cannon;
        private SpeedRail _currentSpeedRail;
        private float _fireRate;
        private float _fireRange;
        private bool _isTwoHandModeOn;
        private bool _isDied;
        private bool _levelStarted;
        private float _tempTimeWeaponOne;
        private float _tempTimeWeaponTwo;

        public Action onPlayerDied;
        
        public void Initialize()
        {
            FillVariables();
            CreateGlove();
            SyncFireRateValue();
            SyncFireRangeValue();
            SyncPowerValue();
            CreateWeapon();
            if (Prefs.CannonLevel != 0)
                FillCannonModel();
        }

        private void Update()
        {
            if (_isDied) return;
            if(_tempTimeWeaponOne > 0)
            {
                var maxRecoil = Quaternion.Euler (-5, 0, 0);
                bodyRight.localRotation = Quaternion.Slerp(bodyRight.localRotation, maxRecoil, Time.deltaTime * _weaponModel.Rate * 2);
                _tempTimeWeaponOne -= Time.deltaTime;
            }
            else
            {
                _tempTimeWeaponOne = 0;
                var minRecoil = Quaternion.Euler (0, 0, 0);
                bodyRight.localRotation = Quaternion.Slerp(bodyRight.localRotation, minRecoil,Time.deltaTime * _weaponModel.Rate * 2);
            }

            if (_cloneSecondWeapon)
            {
                if(_tempTimeWeaponTwo > 0)
                {
                    var maxRecoil = Quaternion.Euler (-5, 0, 0);
                    bodyLeft.localRotation = Quaternion.Slerp(bodyLeft.localRotation, maxRecoil, Time.deltaTime * _weaponModel.Rate * 2);
                    _tempTimeWeaponTwo -= Time.deltaTime;
                }
                else
                {
                    _tempTimeWeaponTwo = 0;
                    var minRecoil = Quaternion.Euler (0, 0, 0);
                    bodyLeft.localRotation = Quaternion.Slerp(bodyLeft.localRotation, minRecoil,Time.deltaTime * _weaponModel.Rate * 2);
                }
            }
        }

        private void CreateGlove()
        {
            if (_rightGlove)
                Destroy(_rightGlove.gameObject);
            var level = Prefs.GloveLevel;
            _gloveModel = ContentManager.Instance.GetGloveModel(level);
            _rightGlove = Instantiate(_gloveModel.GloveRightPrefab, gloveRightSpot);
        }

        public void LevelStarted()
        {
            _levelStarted = true;
        }

        private void FillVariables()
        {
            var currentYear = Prefs.WeaponLevel;
            var model  = ContentManager.Instance.GetProperWeaponModel(currentYear);
            FillModel(model);
            _weaponModel.Year = currentYear;
            yearText.text = currentYear.ToString();
            UIManager.Instance.SyncWeaponUIProgress(_weaponModel.Year, true);
            UIManager.Instance.SetUpgradeButtonsAction(UpgradeApplied);
            UIManager.Instance.onCannonPurchased += FillCannonModel;
            UIManager.Instance.onWeaponUpgraded += WeaponUpgraded;
            objectsTriggerInvoker.onTriggerEnter += ObjectsHandler;
            playerTriggerInvoker.onTriggerEnter += PlayerTriggerEnter;
        }

        private void PlayerTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                YearChanged(-1, Vector3.zero);
            }
        }

        private void ObjectsHandler(Collider obj)
        {
            if (obj.CompareTag("MagazineHandler"))
            {
                var handler = obj.GetComponent<MagazineHandler>();
                if (!handler.CanSweep) return;
                handler.Move();
            }

            if (obj.CompareTag("Enemy"))
            {
                var enemy = obj.GetComponent<Enemy>();
                enemy.ShootActivateHandler(true);
                enemy.Move();
            }
        }

        private void WeaponUpgraded()
        {
            YearChanged(10, Vector3.zero);
        }

        private void FillModel(WeaponModel model)
        {
            _originalWeaponModel = model;
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
        
        private void UpgradeApplied(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.FireRate:
                    SyncFireRateValue();
                    break;
                case UpgradeType.InitYear:
                    YearChanged(1, Vector3.zero);
                    break;
                case UpgradeType.Range:
                    SyncFireRangeValue();
                    break;
                case UpgradeType.Income:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private void SyncFireRateValue()
        {
            _weaponModel.Rate = _originalWeaponModel.Rate + _gloveModel.FireRate + _fireRate + Prefs.FireRateLevel * 0.2f;
        }
        
        private void SyncFireRangeValue()
        {
            _weaponModel.Range = _originalWeaponModel.Range + _gloveModel.Range + _fireRange + Prefs.FireRangeLevel * 0.1f;
        }

        private void SyncPowerValue()
        {
            _weaponModel.Power = _originalWeaponModel.Power + _gloveModel.Power;
        }
        
        private void CreateWeapon()
        {
            DestroyCurrentWeapon();
            _cloneWeapon = Instantiate(_weaponModel.Weapon, weaponRightSpot);
            _cloneWeapon.transform.localPosition = Vector3.zero;
            _cloneWeapon.Initialize(_weaponModel);
            if (_levelStarted)
                ShootActivateHandler(true,true);
            _cloneWeapon.onWeaponHoleTriggerEnter += WeaponHoleTriggerEnter;
            _cloneWeapon.onWeaponTriggerEnter += WeaponTriggerEnter;
            _cloneWeapon.onWeaponTriggerExit += WeaponTriggerExit;
            _cloneWeapon.onBulletShoot += WeaponShoot;
        }

        private void WeaponShoot()
        {
            _tempTimeWeaponOne = 1 / (_weaponModel.Rate * 2);
        }

        public void ShootActivateHandler(bool isActive, bool isFirstWeapon)
        {
            if (isFirstWeapon)
                _cloneWeapon.ShootActivateHandler(isActive);
            else if (_cloneSecondWeapon)
                _cloneSecondWeapon.ShootActivateHandler(isActive);
        }

        public void CannonActiveHandler(bool isActive)
        {
            if (_cannon)
                _cannon.ShootActivateHandler(isActive);
        }
        
        private void DestroyCurrentWeapon()
        {
            if (_cloneWeapon)
            {
                ShootActivateHandler(false,true);
                Destroy(_cloneWeapon.gameObject);
            }
        }

        private void WeaponTriggerEnter(Collider obj)
        {
            if (obj.CompareTag("EndingBlock"))
            {
                if (_isDied) return;
                _isDied = true;
                MoveBack(true);
            }

            if (obj.CompareTag("Obstacle"))
            {
                var obstacle = obj.GetComponent<Obstacle>();
                if (!obstacle.IsObstacleEffective()) return;
                MoveBack(false);
                obstacle.GotHit();
                YearChanged(-1, obstacle.transform.position);
            }

            if (obj.CompareTag("Enemy"))
            {
                var enemy = obj.GetComponent<Enemy>();
                if (enemy.IsHit) return;
                MoveBack(false);
                enemy.GotHitByPlayer();
                YearChanged(-1, enemy.transform.position);

            }

            if (obj.CompareTag("CannonHandler"))
            {
                var cannonHandler = obj.GetComponent<CannonHandler>();
                _cannonModel = cannonHandler.PlayerGotCannon();
                UIManager.Instance.AddCannonPanelOnQueueList();
                CreateCannon();
            }

            if (obj.CompareTag("SpeedRail"))
            {
                _currentSpeedRail = obj.GetComponent<SpeedRail>();
                if (_currentSpeedRail.IsSpeedUp)
                    movement.SpeedUp();
                else 
                    movement.SpeedDown();
            }
        }

        private void WeaponTriggerExit(Collider obj)
        {
            if (obj.CompareTag("SpeedRail"))
            {
                var speedRail = obj.GetComponent<SpeedRail>();
                if (_currentSpeedRail == speedRail)
                    movement.SpeedToNormal();
            }
        }

        private void CreateCannon()
        {
            DestroyCurrentCannon();
            _cannon = Instantiate(_cannonModel.Cannon, cannonSpot);
            _cannon.transform.localScale = Vector3.one * 0.5f;
            _cannon.Initialize(_weaponModel);
            if (_levelStarted)
                CannonActiveHandler(true);
        }

        private void FillCannonModel()
        {
            _cannonModel = ContentManager.Instance.GetCannonModel(Prefs.CannonLevel);
            CreateCannon();
        }

        private void DestroyCurrentCannon()
        {
            if (_cannon)
            {
                CannonActiveHandler(false);
                Destroy(_cannon.gameObject);
            }
        }

        private void MoveBack(bool isDead)
        {
            if (isDead)
                FullStop(true);
            else 
                movement.ZPosStop(true);
            transform.DOMoveZ(transform.localPosition.z - 2, 0.3f).onComplete = () =>
            {
                if (isDead)
                    ApplyDeath();
                else
                    movement.ZPosStop(false);
            };
        }

        public void FullStop(bool isActive)
        {
            movement.FullStop(isActive);
        }

        public void ActiveTwoGun()
        {
            _isTwoHandModeOn = true;
            bodyRight.DOLocalMoveX(0.5f, 0.2f);
            bodyLeft.gameObject.SetActive(true);
            CreateGloveForLeftHand();
            bodyLeft.DOLocalMoveX(-0.5f, 0);
            CreateSecondWeapon();
        }

        private void CreateGloveForLeftHand()
        {
            if (_leftGlove)
                Destroy(_leftGlove.gameObject);
            _leftGlove = Instantiate(_gloveModel.GloveLeftPrefab, gloveLeftSpot);
        }

        private void ApplyDeath()
        {
            ShootActivateHandler(false,true);
            if (_cannon)
                CannonActiveHandler(false);
            var firstTargetRotation = new Vector3(0, 0, -90);
            bodyRight.DOLocalRotate(firstTargetRotation, 0.5f).onComplete = () => onPlayerDied?.Invoke();
            if (_isTwoHandModeOn)
            {
                var secondTargetRotation = new Vector3(0, 0, 90);
                ShootActivateHandler(false,false);
                bodyLeft.DOLocalRotate(secondTargetRotation, 0.5f);
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

            HandleGateEntry(gate.GateTypes, gate.CurrentValue, obj.transform.position);
            
            gate.DestroyItself();
        }
        
        private void BulletGatePassed(Collider obj)
        {
            var gate = obj.GetComponent<BulletGate>();

            HandleGateEntry(GateTypes.Year, gate.GetValue(), Vector3.zero);
            
            gate.PassedFromGate();
        }

        private void HandleGateEntry(GateTypes gateType, float gateValue, Vector3 position)
        {
            switch (gateType)
            {
                case GateTypes.Year:
                    YearChanged(gateValue, position);
                    break;
                case GateTypes.Month:
                    YearChanged(Utility.ConvertMonthIntoYear(gateValue), position);
                    break;
                case GateTypes.Day:
                    YearChanged(Utility.ConvertDayIntoYear(gateValue), position);
                    break;
                case GateTypes.FireRate:
                    FireRateChanged(gateValue);
                    break;
                case GateTypes.FireRange:
                    FireRangeChanged(gateValue);
                    break;
                case GateTypes.Money:
                    IncreaseMoneyFromGate(gateValue, position);
                    break;
                case GateTypes.DUALWEAPON:
                    ActiveTwoGun();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void IncreaseMoneyFromGate(float gateValue, Vector3 position)
        {
            var intVal = Mathf.CeilToInt(gateValue);
            position = Camera.main!.WorldToScreenPoint(position);
            CurrencyHandler.IncreaseMoney(intVal, position, true);
        }

        private void YearChanged(float value, Vector3 position)
        {
            if (value == 0)
                return;
            if (position == Vector3.zero)
                position = new Vector2(Screen.width / 2, Screen.height / 2);
            else 
                position = Camera.main!.WorldToScreenPoint(position);
            var intVal = Mathf.CeilToInt(value);
            UIManager.Instance.CreateFadingText(intVal, position, false);
            _weaponModel.Year += intVal;
            yearText.text = _weaponModel.Year.ToString();
            UIManager.Instance.SyncWeaponUIProgress(_weaponModel.Year, false);
            CheckIfIsThereNewGunAvailable();
        }

        public void IncreaseYear(int value)
        {
            YearChanged(value, Vector3.zero);
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

        private void CreateSecondWeapon()
        {
            DestroySecondWeapon();
            _cloneSecondWeapon = Instantiate(_weaponModel.Weapon, weaponLeftSpot);
            _cloneSecondWeapon.transform.localPosition = Vector3.zero;
            _cloneSecondWeapon.Initialize(_weaponModel);
            _cloneSecondWeapon.onBulletShoot += SecondWeaponShoot;
            if (_levelStarted)
                ShootActivateHandler(true,false);
        }
        
        private void SecondWeaponShoot()
        {
            _tempTimeWeaponTwo = 1 / (_weaponModel.Rate * 2);
        }

        private void ApplyChangesOnModel()
        {
            SyncFireRangeValue();
            SyncFireRateValue();
            SyncPowerValue();
        }

        private void DestroySecondWeapon()
        {
            if (_cloneSecondWeapon)
            {
                ShootActivateHandler(false,false);
                Destroy(_cloneSecondWeapon.gameObject);
            }
        }

        private void FireRateChanged(float value)
        {
            var fixedValue = value / 20;
            _fireRate += fixedValue;
            SyncFireRateValue();
            _cloneWeapon.Initialize(_weaponModel);
        }

        private void FireRangeChanged(float value)
        {
            var fixedValue = value / 100;
            _fireRange += fixedValue;
            SyncFireRangeValue();
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

        public void DeActiveYearTag()
        {
            yearTagObject.SetActive(false);
        }
    }
}