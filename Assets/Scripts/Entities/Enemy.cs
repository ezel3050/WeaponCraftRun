using System;
using TMPro;
using UnityEngine;

namespace Entities
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private TextMeshPro healthText;
        [SerializeField] private Animator animator;
        [SerializeField] private CollectingMoney collectingMoney;
        [SerializeField] private Transform moneyPlacement;
        [SerializeField] private GameObject yearTagObject;
        private static readonly int Death = Animator.StringToHash("death");

        private void Start()
        {
            healthText.text = health.ToString();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
                BulletHit(other);
        }

        private void BulletHit(Collider other)
        {
            if (health <= 0) return;
            var bullet = other.GetComponent<Bullet>();
            bullet.BulletHit();
            health--;
            healthText.text = health.ToString();
            if (health > 0) return;
            animator.SetBool(Death,true);
            var cloneMoney = Instantiate(collectingMoney, moneyPlacement.position, Quaternion.identity);
            cloneMoney.TurnMoneyKinematicOff();
            cloneMoney.transform.localScale = Vector3.one * 0.5f;
            yearTagObject.SetActive(false);
        }
    }
}