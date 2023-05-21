using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public class Magazine : MonoBehaviour
    {
        [SerializeField] private List<MagazineHole> holes;
        [SerializeField] private Transform rotationObject;
        [SerializeField] private int level;

        private float _tempRotationZ;
        private float _tempTime;
        private Quaternion _defaultRotation;

        public int Level => level;
        public Action onMagazineGotFull;

        private void Start()
        {
            _defaultRotation = rotationObject.localRotation;
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
            var model = bullet.GetWeaponModel();
            var availableHole = holes.Find(hole => !hole.IsFilled);
            availableHole.Initialize(model);
            bullet.BulletHit();
            if (!IsEmptyHoleAvailable())
                onMagazineGotFull?.Invoke();
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

        private bool IsEmptyHoleAvailable()
        {
            return holes.Any(hole => !hole.IsFilled);
        }
    }
}