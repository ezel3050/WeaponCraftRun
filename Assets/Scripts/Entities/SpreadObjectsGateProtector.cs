using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Statics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class SpreadObjectsGateProtector : GateProtector
    {
        [SerializeField] private List<Rigidbody> rigidbodies;
        [SerializeField] private float explosionForce;

        private void Start()
        {
            _initObjectCount = rigidbodies.Count;
            _health = _initObjectCount;
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
            var randomObject = rigidbodies.PickRandom();
            randomObject.isKinematic = false;
            randomObject.useGravity = true;
            var pos = randomObject.transform.position;
            pos.z += 1;
            pos.y += Random.Range(-1f, 0f);
            pos.x += Random.Range(-1f, 1f);
            randomObject.AddExplosionForce(explosionForce, pos, 2);
            rigidbodies.Remove(randomObject);
            Destroy(randomObject.gameObject, 5f);
            if (_health > 0) return;
            ShieldBroken();
        }
    }
}