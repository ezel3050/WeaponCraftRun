using System;
using DG.Tweening;
using Statics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemWidgetPanel : MonoBehaviour
    {
        [SerializeField] private Transform lightTransform;
        [SerializeField] private RectMask2D rectMask2D;
        [SerializeField] private TextMeshProUGUI percentText;
        [SerializeField] private float maxMaskPadding;
        [SerializeField] private float rotationSpeed;

        private Vector3 _tempRotation;
        private Vector4 _padding;
        private int _percent;
        private int _level;

        public Vector4 Padding => _padding;

        public Action onIncreaseFinished;

        private void Start()
        {
            _tempRotation = Vector3.zero;
            _padding = Vector4.zero;
            _level = Prefs.ItemWidgetLevel;
            Prefs.ItemWidgetLevel++;
            _percent = _level * 25;
            percentText.text = _percent + "%";
            Invoke("IncreasePercentAndPadding", 1f);
        }

        private void IncreasePercentAndPadding()
        {
            var percentTarget = (_level + 1) * 25;
            _padding.y = (float)_level / 4 * maxMaskPadding;
            rectMask2D.padding = _padding;
            var targetValue = ((float)_level + 1) / 4 * maxMaskPadding;
            DOTween.To(GetPaddingValue, SetPaddingValue, targetValue, 1f);
            DOTween.To(GetPercentValue, SetPercentValue, percentTarget, 1f).onComplete = IncreasingFinished;
        }

        private void Update()
        {
            _tempRotation.z += Time.deltaTime * -rotationSpeed;
            lightTransform.rotation = Quaternion.Euler(_tempRotation);
        }

        public float GetPaddingValue()
        {
            return rectMask2D.padding.y;
        }

        private int GetPercentValue()
        {
            return _percent;
        }

        public void SetPaddingValue(float value)
        {
            _padding.y = value;
            rectMask2D.padding = _padding;
        }
        
        private void SetPercentValue(int value)
        {
            _percent = value;
            percentText.text = _percent + "%";
        }

        private void IncreasingFinished()
        {
            lightTransform.gameObject.SetActive(false);
            onIncreaseFinished?.Invoke();
        }
    }
}