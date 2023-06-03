using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class FadingText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fadingText;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI yearText;
        [SerializeField] private float duration;

        private Sequence _sequence;
        private bool _isMoney;

        public void Initialize(float value, bool isMoney)
        {
            _isMoney = isMoney;
            fadingText.text = (value < 0 ? "" : "+") + value;
            switch (value)
            {
                case 0:
                    return;
                case < 0:
                    fadingText.color = Color.red;
                    yearText.color = Color.red;
                    break;
                case > 0:
                    fadingText.color = Color.green;
                    yearText.color = Color.green;
                    break;
            }

            if (!isMoney)
            {
                yearText.gameObject.SetActive(true);
                icon.gameObject.SetActive(false);
            }
            StartFading();
        }
        
        private void StartFading()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + Screen.height * 0.15f, duration));
            Invoke(nameof(Fade), duration/2);
            _sequence.OnComplete(() =>
            {
                fadingText.DOKill();
                yearText.DOKill();
                icon.DOKill();
                Destroy(gameObject);
            });

        }

        private void Fade()
        {
            fadingText.DOFade(0, duration / 2);
            if (_isMoney)
                icon.DOFade(0, duration / 2);
            else
                yearText.DOFade(0, duration / 2);
        }
    }
}