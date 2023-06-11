using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;

namespace Entities
{
    public class Magazine : MonoBehaviour
    {
        [SerializeField] private List<MagazineHole> holes;
        [SerializeField] private Transform rotationObject;
        [SerializeField] private int level;
        [SerializeField] private TextMeshPro yearText;
        [SerializeField] private GameObject yearTagGameObject;

        private float _tempRotationZ;
        private float _tempTime;
        private Quaternion _defaultRotation;
        private SoundManager _soundManager;

        public int Level => level;
        public Action onMagazineGotFull;

        private void Start()
        {
            _defaultRotation = rotationObject.localRotation;
            yearText.text = GetFilledHolesCount().ToString();
            _soundManager = SoundManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
                BulletHit(other);
        }

        private void BulletHit(Collider other)
        {
            if (!IsEmptyHoleAvailable()) return;
            var bullet = other.GetComponent<Bullet>();
            if (bullet.IsEnemy) return;
            var model = bullet.GetWeaponModel();
            var availableHole = holes.Find(hole => !hole.IsFilled);
            availableHole.Initialize(model);
            bullet.BulletHit();
            _soundManager.BulletHitToMagazine();
            yearText.text = GetFilledHolesCount().ToString();
            if (!IsEmptyHoleAvailable())
            {
                onMagazineGotFull?.Invoke();
                yearTagGameObject.SetActive(false);
            }
            _tempRotationZ += 360 / holes.Count;
            _tempTime = 0;
        }

        private void Update()
        {
            _tempTime += Time.deltaTime;
            var euler = _defaultRotation.eulerAngles;
            var targetRotation = Quaternion.Euler(euler.x, euler.y, euler.z + _tempRotationZ);
            rotationObject.localRotation = Quaternion.Lerp(rotationObject.localRotation, targetRotation, _tempTime);
        }

        public void ReleaseBullets()
        {
            foreach (var hole in holes)
            {
                hole.ActiveGravity();
            }
        }

        public int GetFilledHolesCount()
        {
            var count = holes.FindAll(hole => hole.IsFilled).Count;
            return count;
        }

        private bool IsEmptyHoleAvailable()
        {
            return holes.Any(hole => !hole.IsFilled);
        }
    }
}