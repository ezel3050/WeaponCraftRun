using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class BulletGateSystem : MonoBehaviour
    {
        [SerializeField] private List<BulletGate> bulletGates;
        [SerializeField] private Transform pushingHandTransform;
        [SerializeField] private Transform bulletSpotOnPushingHand;
        [SerializeField] private Transform wholeBulletSpot;
        [SerializeField] private BulletGate bulletGatePrefab;
        [SerializeField] private Vector3 moveTargetForPushingHand;
        [SerializeField] private int maxCapacity;
        [SerializeField] private float bulletMoveAmount;

        private Vector3 _pushingHandInitSpot;
        private BulletGate _currentBullet;

        private void Start()
        {
            _pushingHandInitSpot = pushingHandTransform.localPosition;
            Invoke("CreateBullet" , 3f);
        }

        public void CreateBullet()
        {
            if (bulletGates.Count >= maxCapacity) return;
            _currentBullet = Instantiate(bulletGatePrefab, bulletSpotOnPushingHand);
            _currentBullet.transform.localPosition = Vector3.zero;
            MovePushingHand();
            MoveBulletList();
            bulletGates.Add(_currentBullet);
        }

        private void MoveBulletList()
        {
            foreach (var bullet in bulletGates)
            {
                bullet.transform.DOLocalMoveX(bullet.transform.localPosition.x + bulletMoveAmount, 0.5f);
            }
        }

        private void MovePushingHand()
        {
            pushingHandTransform.DOLocalMove(moveTargetForPushingHand, 0.5f).onComplete = PushingHandReached;
        }

        private void PushingHandReached()
        {
            pushingHandTransform.DOLocalMove(_pushingHandInitSpot, 0.5f).onComplete = CreateBullet;
            var bulletTransform = _currentBullet.transform;
            bulletTransform.SetParent(wholeBulletSpot);
            bulletTransform.localPosition = Vector3.zero;
        }
    }
}