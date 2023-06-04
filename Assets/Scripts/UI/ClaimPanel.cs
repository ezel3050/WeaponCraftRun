using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ClaimPanel : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Transform lightTransform;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Button claimButton;

        private Vector3 _tempRotation;
        private Action _callback;

        private void Start()
        {
            _tempRotation = Vector3.zero;
            claimButton.onClick.AddListener(ClaimButtonClicked);
        }

        public void Initialize(Sprite icon, Action callback)
        {
            iconImage.sprite = icon;
            iconImage.SetNativeSize();
            _callback = callback;
        }
        
        private void Update()
        {
            _tempRotation.z += Time.deltaTime * -rotationSpeed;
            lightTransform.rotation = Quaternion.Euler(_tempRotation);
        }

        private void ClaimButtonClicked()
        {
            _callback?.Invoke();
        }
    }
}