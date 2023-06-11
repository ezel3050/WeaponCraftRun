using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Entities
{
    public class ChainProtector : GateProtector
    {
        [SerializeField] private float health;
        [SerializeField] private float explosionForce;
        [SerializeField] private float explosionRange;
        [SerializeField] private List<Rigidbody> chainRigidbodiesOne;
        [SerializeField] private List<Rigidbody> chainRigidbodiesTwo;
        [SerializeField] private List<Rigidbody> lockRigidbodies;
        [SerializeField] private HingeJoint connectorOne;
        [SerializeField] private HingeJoint connectorTwo;
        [SerializeField] private List<MeshRenderer> lockBeforeBreak;
        [SerializeField] private GameObject lockAfterBreak;
        private void Start()
        {
            _health = health;
            _initObjectCount = _health;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
                BulletHit(other);
        }

        private void ExplodeChain()
        {
            foreach (var chainRigidBody in chainRigidbodiesOne)
            {
                var pos = chainRigidBody.position;
                pos.z++;
                chainRigidBody.AddExplosionForce(explosionForce, pos, explosionRange);
            }

            foreach (var chainRigidBody in chainRigidbodiesTwo)
            {
                var pos = chainRigidBody.position;
                pos.z++;
                chainRigidBody.AddExplosionForce(explosionForce, pos, explosionRange);
            }
        }   

        protected override void BulletHit(Collider other)
        {
            if (_health <= 0) return;
            base.BulletHit(other);
            ExplodeChain();
            _soundManager.BulletHitToChainGate();
            if (_health > 0) return;
            _soundManager.ChainGateBroke();
            ShieldBroken();
        }

        protected override void ShieldBroken()
        {
            base.ShieldBroken();
            Destroy(connectorOne);
            Destroy(connectorTwo);
            foreach (var lockRenderer in lockBeforeBreak)
            {
                lockRenderer.enabled = false;
            }
            lockAfterBreak.SetActive(true);
            foreach (var lockRigidbody in lockRigidbodies)
            {
                var pos = lockRigidbody.transform.position;
                pos.z += 1;
                pos.y += Random.Range(-1f, 0f);
                pos.x += Random.Range(-1f, 1f);
                lockRigidbody.AddExplosionForce(explosionForce / 2, pos, explosionRange);
            }
        }
    }
}