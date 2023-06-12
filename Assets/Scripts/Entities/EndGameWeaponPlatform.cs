using System;
using DefaultNamespace;
using DefaultNamespace.Entities;
using DG.Tweening;
using Level;
using Managers;
using Models;
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
        private WeaponModel _currentModel;
        private bool _isPassedOnce;

        public Action onPassedEndGamePlatform;

        private void Start()
        {
            _tempRotation = Vector3.zero;
            finishLine.onFinishLinePassed += PassedFinishedLine;
            var level = Prefs.EndGameWeaponLevel;
            _currentModel = ContentManager.Instance.GetEndingWeaponModel(level);
            _weapon = Instantiate(_currentModel.Weapon, weaponSpot);
        }

        private void Update()
        {
            _tempRotation.y += Time.deltaTime * -rotationSpeed;
            _weapon.transform.localRotation = Quaternion.Euler(_tempRotation);
        }

        private void PassedFinishedLine()
        {
            if (_isPassedOnce) return;
            _isPassedOnce = true;
            var currentLevel = (MasterGunLevel) LevelManager.CurrentLevel;
            currentLevel.StopPlayer();
            currentLevel.DisableShooting();
            SoundManager.Instance.EndGame();
            Prefs.IsTwoGunAvailableOnNextLevel = true;
            Invoke("OpenPanel", 2f);
        }

        private void OpenPanel()
        {
            UIManager.Instance.OpenEndGameWeaponPanel(_currentModel.WeaponSprite, ClaimButtonClicked);
        }

        private void ClaimButtonClicked()
        {
            onPassedEndGamePlatform?.Invoke();
        }
    }
}