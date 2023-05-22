using System;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using Entities;
using Statics;
using UnityEngine;

namespace Components
{
    public class MagazineHandler : MonoBehaviour
    {
        [SerializeField] private List<Magazine> magazines;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float railSpeed;

        private int _currentMagazineLevel;
        private Magazine _currentMagazine;
        private bool _isDismissed;

        public Action<MagazineHandler> onMagazineGotFull;
        private void Start()
        {
            FillVariables();
            SubscribeAction();
            ActivateMagazine();
        }

        private void FillVariables()
        {
            _currentMagazineLevel = Prefs.MagazineLevel;
        }
        
        private void SubscribeAction()
        {
            foreach (var magazine in magazines)
            {
                magazine.onMagazineGotFull += MagazineGotFull;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isDismissed) return;
            if (!other.CompareTag("Player") && !other.CompareTag("Weapon")) return;
            _isDismissed = true;
            onMagazineGotFull?.Invoke(this);
        }

        private void ActivateMagazine()
        {
            DeActiveAllMagazines();
            _currentMagazine = magazines.Find(magazine => magazine.Level == _currentMagazineLevel);
            _currentMagazine.gameObject.SetActive(true);
        }

        private void MagazineGotFull()
        {
            _isDismissed = true;
            onMagazineGotFull?.Invoke(this);
        }

        public void JumpOnRail(float railXPos)
        {
            var localPos = transform.localPosition;
            var targetPos = new Vector3(railXPos, localPos.y, localPos.z);
            transform.DOJump(targetPos, 2, 1, 1f).onComplete = JumpedOnRail;
        }

        private void JumpedOnRail()
        {
            rb.velocity = Vector3.forward * railSpeed;
        }

        public void StopMoving()
        {
            rb.velocity = Vector3.zero;
        }

        public void Rotate(float value)
        {
            var target = new Vector3(value, 0, 0);
            transform.DOLocalRotate(target, 0.3f).onComplete = ReleaseBullets;
        }

        public void ManualMove(Vector3 target)
        {
            transform.DOMove(target, 0.3f);
        }

        public void ShrinkSize()
        {
            var target = Vector3.one / 2;
            transform.DOScale(target, 0.3f);
        }

        public int GetFilledHolesCount()
        {
            return _currentMagazine.GetFilledHolesCount();
        }

        private void ReleaseBullets()
        {
            _currentMagazine.ReleaseBullets();
            MoveAwayMagazine();
        }

        private void MoveAwayMagazine()
        {
            transform.DOMoveX(-10, 1f).onComplete = DestroyItself;
        }

        private void DeActiveAllMagazines()
        {
            foreach (var magazine in magazines)
            {
                magazine.gameObject.SetActive(false);
            }
        }

        private void DestroyItself()
        {
            Destroy(gameObject);
        }
    }
}