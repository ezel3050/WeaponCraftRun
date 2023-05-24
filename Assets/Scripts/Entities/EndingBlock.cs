using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Entities
{
    public class EndingBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshPro countText;
        [SerializeField] private MeshRenderer blockRenderer;
        [SerializeField] private CapsuleCollider blockCollider;
        [SerializeField] private Rigidbody moneyRigidbody;
        [SerializeField] private ParticleSystem destroyParticle;
        [SerializeField] private int value;

        private void Start()
        {
            countText.text = value.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
                BulletHit(other);
        }

        private void BulletHit(Collider other)
        {
            var bullet = other.GetComponent<Bullet>();
            var model = bullet.GetWeaponModel();
            bullet.BulletHit();
            value -= model.Power;
            if (value <= 0)
            {
                blockRenderer.enabled = false;
                blockCollider.enabled = false;
                countText.enabled = false;
                destroyParticle.Play();
                moneyRigidbody.isKinematic = false;
            }
            else
            {
                countText.text = value.ToString();
            }
        }

        public void SetValue(int x)
        {
            value = x;
            countText.text = value.ToString();
        }
    }
}