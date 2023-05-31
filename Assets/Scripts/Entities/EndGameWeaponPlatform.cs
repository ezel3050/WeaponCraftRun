using System;
using DefaultNamespace.Entities;
using DG.Tweening;
using Managers;
using Statics;
using UnityEngine;

namespace Entities
{
    public class EndGameWeaponPlatform : MonoBehaviour
    {
        [SerializeField] private Transform weaponSpot;
        [SerializeField] private FinishLine finishLine;
        [SerializeField] private float rotationSpeed;

        private Weapon _weapon;
        private Vector3 _tempRotation;

        private void Start()
        {
            _tempRotation = Vector3.zero;
            finishLine.onFinishLinePassed += PassedFinishedLine;
            var level = Prefs.EndGameWeaponLevel;
            var model = ContentManager.Instance.GetEndingWeaponModel(level);
            _weapon = Instantiate(model.Weapon, weaponSpot);
        }

        private void Update()
        {
            _tempRotation.y += Time.deltaTime * -rotationSpeed;
            _weapon.transform.localRotation = Quaternion.Euler(_tempRotation);
        }

        private void PassedFinishedLine()
        {
            Prefs.EndGameWeaponLevel++;
        }
    }
}