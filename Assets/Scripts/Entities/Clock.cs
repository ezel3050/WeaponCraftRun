using System;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private Transform handle;
        [SerializeField] private ParticleSystem clockParticle;
        [SerializeField] private Collider clockCollider;
        [SerializeField] private float minHandleSpeed;
        [SerializeField] private float maxHandleSpeed;
        [SerializeField] private float resetTime;

        private bool _canCountDown;
        private float _tempTime;
        private float _targetSpeed;
        private Vector3 _tempRotation;

        public Action<int> onBulletHit;

        private void Start()
        {
            _tempRotation = Vector3.zero;
            _targetSpeed = minHandleSpeed;
        }

        private void Update()
        {
            HandleRotation();
            ResetHandler();
        }

        private void HandleRotation()
        {
            _tempRotation.z += Time.deltaTime * _targetSpeed;
            handle.transform.localRotation = Quaternion.Euler(_tempRotation);
        }

        private void ResetHandler()
        {
            if (!_canCountDown) return;
            _tempTime += Time.deltaTime;
            if (_tempTime < resetTime) return;
            _tempTime = 0;
            clockParticle.Stop();
            _canCountDown = false;
            _targetSpeed = minHandleSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
                BulletHit(other);
            if (other.CompareTag("Skip") || other.CompareTag("Weapon"))
            {
                clockCollider.enabled = false;
                transform.DOScale(Vector3.zero, 0.5f);
            }
        }

        private void BulletHit(Collider other)
        {
            var bullet = other.GetComponent<Bullet>();
            if (bullet.IsEnemy) return;
            bullet.BulletHit();
            clockParticle.Play();
            _canCountDown = true;
            _tempTime = 0;
            _targetSpeed = maxHandleSpeed;
            onBulletHit?.Invoke(1);
        }
    }
}