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

        private int _currentMagazineLevel;

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

        private void ActivateMagazine()
        {
            DeActiveAllMagazines();
            var targetMagazine = magazines.Find(magazine => magazine.Level == _currentMagazineLevel);
            targetMagazine.gameObject.SetActive(true);
        }

        private void MagazineGotFull()
        {
            onMagazineGotFull?.Invoke(this);
        }

        public void JumpOnRail(float railXPos)
        {
            var localPos = transform.localPosition;
            var targetPos = new Vector3(railXPos, localPos.y, localPos.z);
            transform.DOJump(targetPos, 2, 1, 1f);
        }

        private void DeActiveAllMagazines()
        {
            foreach (var magazine in magazines)
            {
                magazine.gameObject.SetActive(false);
            }
        }
    }
}