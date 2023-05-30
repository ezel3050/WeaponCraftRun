using System;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Level;
using Managers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIWeaponProgress : MonoBehaviour
    {
        [SerializeField] private Image currentImage;
        [SerializeField] private Image nextImage;
        [SerializeField] private TextMeshProUGUI currentYearText;
        [SerializeField] private TextMeshProUGUI nextYearText;
        [SerializeField] private Transform progressBarTransform;
        [SerializeField] private Transform zeroPercentProgressBarSpot;
        [SerializeField] private Transform fullProgressBarSpot;

        private List<WeaponModel> _modelList;
        private int _currentYear;
        private bool _isInit;

        private void Start()
        {
            _isInit = true;
        }

        public void Initialize(int year, bool isFirstInit)
        {
            if (isFirstInit)
                _currentYear = 0;
            _modelList = ContentManager.Instance.GetTwoSideModel(year);
            InitializeCurrentState();
            InitializeNextState();
            SyncProgressBar(year);
        }

        private void InitializeCurrentState()
        {
            currentImage.sprite = _modelList[0].WeaponSprite;
            currentYearText.text = _modelList[0].Year.ToString();
        }
        
        private void InitializeNextState()
        {
            nextImage.sprite = _modelList[1].WeaponBlackSprite;
            nextYearText.text = _modelList[1].Year.ToString();
        }

        private void SyncProgressBar(int year)
        {
            var currentYearDividedByTen = _currentYear / 10;
            var nextYearDividedByTen = year / 10;
            if (nextYearDividedByTen > currentYearDividedByTen)
                progressBarTransform.position = zeroPercentProgressBarSpot.position;
            var zeroToTenScale = year % 10;
            var zeroPosition = zeroPercentProgressBarSpot.position;
            var wholeX = fullProgressBarSpot.position.x - zeroPosition.x;
            var target = (wholeX / 10) * zeroToTenScale;
            _currentYear = year;
            if (_isInit)
                progressBarTransform.DOMoveX(zeroPosition.x + target, 0.3f);
            else
            {
                var pos = progressBarTransform.position;
                progressBarTransform.position = new Vector3(zeroPosition.x + target, pos.y, pos.z);
            }
        }
    }
}