using System;
using DG.Tweening;
using Managers;
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
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image mainIcon;
        [SerializeField] private Image blackIcon;
        [SerializeField] private float maxMaskPadding;
        [SerializeField] private float rotationSpeed;

        private Vector3 _tempRotation;
        private Vector4 _padding;
        private int _percent;
        private int _level;
        private bool _is100Percent;

        public Vector4 Padding => _padding;

        public Action<bool> onIncreaseFinished;

        private void Start()
        {
            var gloveLevel = Prefs.GloveLevel;
            var gloveModel = ContentManager.Instance.GetGloveModel(gloveLevel + 1);
            mainIcon.sprite = gloveModel.MainSprite;
            blackIcon.sprite = gloveModel.ShadowSprite;
            _tempRotation = Vector3.zero;
            _padding = Vector4.zero;
            _level = Prefs.ItemWidgetLevel;
            _percent = _level * 25;
            percentText.text = _percent + "%";
            _padding.y = (float)_level / 4 * maxMaskPadding;
            rectMask2D.padding = _padding;
            if (Prefs.ItemWidgetLevel < 4)
                Prefs.ItemWidgetLevel++;
            else
            {
                lightTransform.gameObject.SetActive(false);
                onIncreaseFinished?.Invoke(true);
                return;
            }
            Invoke("IncreasePercentAndPadding", 1f);
        }

        private void IncreasePercentAndPadding()
        {
            var percentTarget = (_level + 1) * 25;
            _is100Percent = (_level + 1) * 25 == 100;
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
            descriptionText.gameObject.SetActive(false);
            onIncreaseFinished?.Invoke(_is100Percent);
        }
    }
}