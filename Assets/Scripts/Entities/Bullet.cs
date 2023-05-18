using System;
using Models;
using UnityEngine;

namespace Entities
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem hitParticle;
        [SerializeField] private Rigidbody bulletRigidbody;
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
                DestroyItself();
        }

        private void OnTriggerEnter(Collider other)
        {
            _canCountDown = false;
            _tempTime = 0;
            hitParticle.Play();
        }

        private void DestroyItself()
        {
            Destroy(gameObject);
        }
    }
}