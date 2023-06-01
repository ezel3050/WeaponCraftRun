using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Statics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class BrickGateProtector : GateProtector
    {
        [SerializeField] private List<Rigidbody> rigidbodies;
        [SerializeField] private float outsideProgressXPos;
        [SerializeField] private Transform innerProgressBar;
        [SerializeField] private GameObject wholeProgressBarObject;
        [SerializeField] private Collider objectCollider;

        private int _initBrickCount;
        private float _health;

        private void Start()
        {
            _initBrickCount = rigidbodies.Count;
            _health = _initBrickCount;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
                BulletHit(other);
        }

        private void BulletHit(Collider other)
        {
            if (_health <= 0) return;
            var bullet = other.GetComponent<Bullet>();
            bullet.BulletHit();
            _health--;
            var healthPercent = _health / _initBrickCount;
            var targetProgressXPos = outsideProgressXPos * (1 - healthPercent);
            innerProgressBar.localPosition = new Vector3(targetProgressXPos, 0, 0);
            var randomBrick = rigidbodies.PickRandom();
            randomBrick.isKinematic = false;
            randomBrick.useGravity = true;
            var pos = randomBrick.transform.position;
            pos.z += 1;
            pos.y += Random.Range(-1f, 0f);
            pos.x += Random.Range(-1f, 1f);
            randomBrick.AddExplosionForce(500, pos, 2);
            rigidbodies.Remove(randomBrick);
            Destroy(randomBrick.gameObject, 5f);
            if (_health > 0) return;
            ShieldBroken();
        }

        protected override void ShieldBroken()
        {
            base.ShieldBroken();
            objectCollider.enabled = false;
            wholeProgressBarObject.SetActive(false);
        }
    }
}