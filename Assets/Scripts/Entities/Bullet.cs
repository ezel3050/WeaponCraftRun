using System;
using Models;
using UnityEngine;

namespace Entities
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem hitParticle;
        [SerializeField] private Rigidbody bulletRigidbody;
        [SerializeField] private GameObject visual;
        [SerializeField] private Collider bulletCollider;
        [SerializeField] private float speed = 10f;

        private WeaponModel _weaponModel;
        private float _tempTime;
        private bool _canCountDown;
        public void Initialize(WeaponModel weaponModel)
        {
            _weaponModel = weaponModel;
            bulletRigidbody.velocity = Vector3.forward * speed;
            _canCountDown = true;
        }

        private void Update()
        {
            if (!_canCountDown) return;
            _tempTime += Time.deltaTime;
            if (_tempTime >= _weaponModel.Range)
                DisableItself();
        }

        public WeaponModel GetWeaponModel()
        {
            return _weaponModel;
        }

        public void BulletHit()
        {
            _canCountDown = false;
            _tempTime = 0;
            hitParticle.Play();
            bulletCollider.enabled = false;
            DisableItself();
        }

        private void DisableItself()
        {
            bulletRigidbody.velocity = Vector3.zero;
            visual.gameObject.SetActive(false);
            Invoke(nameof(DestroyItself), 1f);
        }

        private void DestroyItself()
        {
            Destroy(gameObject);
        }
    }
}