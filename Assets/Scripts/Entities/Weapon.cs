using System;
using System.Collections;
using Components;
using Models;
using Sirenix.OdinInspector;
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

        public Action<Collider> onTriggerEnter;

        [Button]
        public void besmellah()
        {
            var q = shootingSpot.gameObject.AddComponent<SphereCollider>();
            q.radius = 0.1f;
            q.isTrigger = true;
            gunInvoker = shootingSpot.gameObject.AddComponent<TriggerInvoker>();
            shootingSpot.gameObject.tag = "WeaponPoint";
        }

        private void Start()
        {
            gunInvoker.onTriggerEnter += TriggerEnter;
        }

        public void Initialize(WeaponModel weaponModel)
        {
            _weaponModel = weaponModel;
            _waitingTime = new WaitForSeconds(1 / _weaponModel.Rate);
        }

        private void TriggerEnter(Collider other)
        {
            onTriggerEnter?.Invoke(other);
        }

        public void ShootActivateHandler(bool isOn)
        {
            if (isOn)
                StartCoroutine(Shooting());
            else 
                StopCoroutine(Shooting());
        }

        private IEnumerator Shooting()
        {
            while (true)
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