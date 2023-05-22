using System;
using System.Collections.Generic;
using Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class BulletGateSystem : MonoBehaviour
    {
        [SerializeField] private List<GateBullet> gateBullets;
        [SerializeField] private List<BulletGate> bulletGates;
        [SerializeField] private List<int> gateYears;
        [SerializeField] private Transform pushingHandTransform;
        [SerializeField] private Transform bulletSpotOnPushingHand;
        [SerializeField] private Transform wholeBulletSpot;
        [SerializeField] private Transform magazineSpot;
        [SerializeField] private GateBullet gateBulletPrefab;
        [SerializeField] private Vector3 moveTargetForPushingHand;
        [SerializeField] private Vector3 spreadBulletYZPos;
        [SerializeField] private TriggerInvoker gateBulletTaker;
        [SerializeField] private int maxCapacity;
        [SerializeField] private float bulletMoveAmount;
        [SerializeField] private float spreadBulletMoveAmount;

        private Vector3 _pushingHandInitSpot;
        private GateBullet _currentGateBullet;
        private float _coefficient = 1.4f;
        private float _wholeResults = 0;
        private int _bulletToCreate;
        private bool _passedOnce;

        private void Start()
        {
            _pushingHandInitSpot = pushingHandTransform.localPosition;
            
            for (var i = 0; i < bulletGates.Count; i++)
            {
                bulletGates[i].onPassedFromGate += PlayerPassedFromGate;
                bulletGates[i].Initialize(gateYears[i]);
            }

            gateBulletTaker.onTriggerEnter += GateTookBullet;
        }

        private void GateTookBullet(Collider obj)
        {
            if (_passedOnce)
            {
                _bulletToCreate = 0;
                return;
            }
            if (!obj.CompareTag("Bullet")) return;
            var bullet = obj.GetComponent<Bullet>();
            bullet.BulletHit();
            AddBulletsBaseOnMagazine(1);
        }

        private void PlayerPassedFromGate(int gateNumber, bool isActive)
        {
            if (_passedOnce) return;
            _passedOnce = true;
            gateBullets.Reverse();
            var bulletCounts = gateBullets.Count;
            var firstBulletIndex = (gateNumber - 1) * 5;
            if (firstBulletIndex >= bulletCounts) return;
            var targetBulletList = new List<GateBullet>();
            if (firstBulletIndex + 5 <= bulletCounts)
            {
                targetBulletList.AddRange(gateBullets.GetRange(firstBulletIndex, 5));
            }
            if (!isActive)
            {
                var indexToCheck = bulletCounts - firstBulletIndex;
                targetBulletList.AddRange(gateBullets.GetRange(firstBulletIndex, indexToCheck));
            }
            
            if (isActive)
            {
                var firstBullet = targetBulletList[0];
                var targetXPos = firstBullet.transform.localPosition.x - bulletMoveAmount * 2;
                var targetPos = new Vector3(targetXPos, spreadBulletYZPos.y, spreadBulletYZPos.z);
                foreach (var bullet in targetBulletList)
                {
                    targetPos.x += spreadBulletMoveAmount;
                    bullet.PlayWinSequence(targetPos);
                }
            }
            else
            {
                foreach (var bullet in targetBulletList)
                {
                    bullet.PlayLoseSequence();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("MagazineHandler")) return;
            var magazineHandler = other.GetComponent<MagazineHandler>();
            magazineHandler.StopMoving();
            magazineHandler.Rotate(-90);
            magazineHandler.ManualMove(magazineSpot.position);
            magazineHandler.ShrinkSize();
            AddBulletsBaseOnMagazine(magazineHandler.GetFilledHolesCount());
        }

        private void AddBulletsBaseOnMagazine(int count)
        {
            var intResultBefore = Mathf.FloorToInt(_wholeResults);
            var result = count / _coefficient;
            _wholeResults += result;
            var intResultNew = Mathf.FloorToInt(_wholeResults);
            _bulletToCreate += intResultNew - intResultBefore;
            LoopHandler();
        }

        private void LoopHandler()
        {
            if (_bulletToCreate <= 0) return;
            _bulletToCreate--;
            CreateBullet();
        }
        private void CreateBullet()
        {
            if (gateBullets.Count >= maxCapacity) return;
            _currentGateBullet = Instantiate(gateBulletPrefab, bulletSpotOnPushingHand);
            _currentGateBullet.transform.localPosition = Vector3.zero;
            MovePushingHand();
            MoveBulletList();
            gateBullets.Add(_currentGateBullet);
            CheckToActiveGate();
        }

        private void MoveBulletList()
        {
            foreach (var bullet in gateBullets)
            {
                bullet.transform.DOLocalMoveX(bullet.transform.localPosition.x + bulletMoveAmount, 0.1f);
            }
        }

        private void MovePushingHand()
        {
            pushingHandTransform.DOLocalMove(moveTargetForPushingHand, 0.1f).onComplete = PushingHandReached;
        }

        private void PushingHandReached()
        {
            pushingHandTransform.DOLocalMove(_pushingHandInitSpot, 0.1f).onComplete = LoopHandler;
            var bulletTransform = _currentGateBullet.transform;
            bulletTransform.SetParent(wholeBulletSpot);
            bulletTransform.localPosition = Vector3.zero;
        }

        private void CheckToActiveGate()
        {
            var bulletCount = gateBullets.Count;
            if (bulletCount % 5 != 0) return;
            var gateNumber = bulletCount / 5;
            bulletGates[gateNumber - 1].Active();
        }
    }
}