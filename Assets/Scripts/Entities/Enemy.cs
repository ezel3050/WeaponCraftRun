using System;
using System.Collections;
using Components;
using Models;
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
        [SerializeField] private Bullet enemyBullet;
        [SerializeField] private Transform shootingPosition;
        [SerializeField] private Sweeper sweeper;
        [SerializeField] private bool canMove;
        [SerializeField] private bool canShoot;
        private static readonly int Death = Animator.StringToHash("death");
        private static readonly int Walk = Animator.StringToHash("walk");

        private bool _isShooting;
        private bool _gotHitByPlayer;
        private WaitForSeconds _waitingTime;

        public bool IsHit => _gotHitByPlayer;

        private void Start()
        {
            healthText.text = health.ToString();
            _waitingTime = new WaitForSeconds(1f);
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
            sweeper.enabled = false;
            _gotHitByPlayer = true;
            ShootActivateHandler(false);
            canMove = false;
            var cloneMoney = Instantiate(collectingMoney, moneyPlacement.position, Quaternion.identity);
            cloneMoney.TurnMoneyKinematicOff();
            cloneMoney.transform.localScale = Vector3.one * 0.5f;
            yearTagObject.SetActive(false);
        }
        
        public void ShootActivateHandler(bool isOn)
        {
            if (!canShoot) return;
            if (isOn)
            {
                _isShooting = true;
                StartCoroutine(Shooting());
            }
            else
            {
                _isShooting = false;
                StopCoroutine(Shooting());
            }
        }

        public void Move()
        {
            if (canMove)
            {
                animator.SetBool(Walk, true);
                sweeper.enabled = true;
            }
        }
        
        private IEnumerator Shooting()
        {
            while (_isShooting)
            {
                CreateBullet();
                yield return _waitingTime;
            }
        }
        
        private void CreateBullet()
        {
            var cloneBullet = Instantiate(enemyBullet);
            cloneBullet.transform.position = shootingPosition.position;
            cloneBullet.transform.rotation = Quaternion.Euler(new Vector3(0,-180,0));
            var model = new WeaponModel()
            {
                Range = 0.5f
            };
            cloneBullet.Initialize(model, true);
        }

        public void GotHitByPlayer()
        {
            _gotHitByPlayer = true;
        }
    }
}