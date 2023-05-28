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

        public void Initialize(int year)
        {
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
            var wholeX = fullProgressBarSpot.position.x - zeroPercentProgressBarSpot.position.x;
            var target = (wholeX / 10) * zeroToTenScale;
            _currentYear = year;
            progressBarTransform.DOMoveX(progressBarTransform.position.x + target, 0.3f);
        }
    }
}