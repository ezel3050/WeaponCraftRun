using System;
using Components;
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
        [SerializeField] private BoxCollider moneyCollider;
        [SerializeField] private Rigidbody moneyRigidbody;
        [SerializeField] private ParticleSystem destroyParticle;
        [SerializeField] private ScaleBouncer scaleBouncer;
        [SerializeField] private float value;

        public Action onExploded;

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
            scaleBouncer.Poke();
            value -= model.Power;
            value = Mathf.CeilToInt(value);
            if (value <= 0)
            {
                blockRenderer.enabled = false;
                blockCollider.enabled = false;
                countText.enabled = false;
                destroyParticle.Play();
                moneyRigidbody.isKinematic = false;
                moneyCollider.enabled = true;
                onExploded?.Invoke();
            }
            else
            {
                countText.text = value.ToString();
            }
        }
    }
}