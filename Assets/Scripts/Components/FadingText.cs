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
        [SerializeField] private float duration;

        private Sequence _sequence;

        public void Initialize(string text)
        {
            fadingText.text = "+" + text;
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
                icon.DOKill();
                Destroy(gameObject);
            });

        }

        private void Fade()
        {
            fadingText.DOFade(0, duration / 2);
            icon.DOFade(0, duration / 2);
        }
    }
}