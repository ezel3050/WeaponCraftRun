using System;
using System.Collections;
using Components;
using Models;
using UnityEngine;

namespace DefaultNamespace.Entities
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform shootingSpot;
        [SerializeField] private ParticleSystem shootingParticle;
        [SerializeField] private TriggerInvoker gunInvoker;

        private WeaponModel _weaponModel;
        private WaitForSeconds _waitingTime;
        private bool _isShooting;

        public Action<Collider> onWeaponHoleTriggerEnter;
        public Action<Collider> onWeaponTriggerEnter;

        private void Start()
        {
            gunInvoker.onTriggerEnter += WeaponHoleTriggerEnter;
        }

        public void Initialize(WeaponModel weaponModel)
        {
            _weaponModel = weaponModel;
            _waitingTime = new WaitForSeconds(1 / _weaponModel.Rate);
        }

        private void WeaponHoleTriggerEnter(Collider other)
        {
            onWeaponHoleTriggerEnter?.Invoke(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            onWeaponTriggerEnter?.Invoke(other);
        }

        public void ShootActivateHandler(bool isOn)
        {
            if (isOn)
            {
                _isShooting = true;
                StartCoroutine(Shooting());
            }
            else
            {
                _isShooting = false;
                StopCoroutine(Shooting());
            }
        }

        private IEnumerator Shooting()
        {
            while (_isShooting)
            {
                CreateBullet();
                yield return _waitingTime;
            }
        }

        private void CreateBullet()
        {
            var cloneBullet = Instantiate(_weaponModel.Bullet);
            cloneBullet.transform.position = shootingSpot.position;
            cloneBullet.Initialize(_weaponModel);
            shootingParticle.Play();
        }
    }
}