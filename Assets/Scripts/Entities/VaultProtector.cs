using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class VaultProtector : GateProtector
    {
        [SerializeField] private float health;
        [SerializeField] private Transform rotationObject;
        [SerializeField] private List<Rigidbody> vaultRigidbodies;

        private float _tempTime;
        private float _tempRotationZ;
        private Quaternion _defaultRotation;

        private void Start()
        {
            _health = health;
            _initObjectCount = _health;
            _defaultRotation = rotationObject.localRotation;
        }
        
        private void Update()
        {
            _tempTime += Time.deltaTime;
            var euler = _defaultRotation.eulerAngles;
            var targetRotation = Quaternion.Euler(euler.x, euler.y, euler.z + _tempRotationZ);
            rotationObject.localRotation = Quaternion.Lerp(rotationObject.localRotation, targetRotation, _tempTime);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
                BulletHit(other);
        }

        protected override void BulletHit(Collider other)
        {
            if (_health <= 0) return;
            base.BulletHit(other);
            _tempRotationZ += 40;
            _tempTime = 0;
            if (_health > 0) return;
            ShieldBroken();
        }

        protected override void ShieldBroken()
        {
            base.ShieldBroken();
            foreach (var vaultRigidbody in vaultRigidbodies)
            {
                vaultRigidbody.isKinematic = false;
                var pos = vaultRigidbody.transform.position;
                pos.z += 1;
                pos.y += Random.Range(-1f, 0f);
                pos.x += Random.Range(-1f, 1f);
                vaultRigidbody.AddExplosionForce(500, pos, 5);
            }
        }
    }
}