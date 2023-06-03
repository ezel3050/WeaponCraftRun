using System.Collections;
using Models;
using UnityEngine;

namespace Entities
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] public float fireRate;
        [SerializeField] private Transform shootingSpot;
        [SerializeField] private Bullet bullet;
        
        private WaitForSeconds _waitingTime;
        private bool _isShooting;
        private WeaponModel _weaponModel;
        private float _calculatedFireRate;

        public void Initialize(WeaponModel weaponModel)
        {
            _weaponModel = weaponModel;
            _calculatedFireRate = fireRate / 20;
            _waitingTime = new WaitForSeconds(1 / _calculatedFireRate);
        }
        
        public void ShootActivateHandler(bool isOn)
        {
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
            var cloneBullet = Instantiate(bullet);
            cloneBullet.transform.position = shootingSpot.position;
            cloneBullet.Initialize(_weaponModel);
            // onBulletShoot?.Invoke();
        }
    }
}