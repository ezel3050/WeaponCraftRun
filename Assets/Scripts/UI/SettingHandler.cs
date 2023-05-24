using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingHandler : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private Transform backgroundTransform;
        [SerializeField] private Transform backgroundCloseSpot;
        [SerializeField] private Transform backgroundOpenSpot;

        private bool _isOpen;
        private void Start()
        {
            btn.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            _isOpen = !_isOpen;
            if (_isOpen)
            {
                var targetRotation = new Vector3(0, 0, 90);
                btn.transform.DOLocalRotate(targetRotation, 0.5f);
            }
            else
            {
                btn.transform.DOLocalRotate(Vector3.zero, 0.5f);
            }
            backgroundTransform.DOMove(_isOpen ? backgroundOpenSpot.position : backgroundCloseSpot.position, 0.5f);
        }
    }
}