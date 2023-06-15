using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MessageBox : Panel
    {
    
        [Space]
        [SerializeField] private TMP_Text titleTMP;
        [SerializeField] private TMP_Text messageTMP;
        [SerializeField] private TMP_Text messageSmallTMP;
        [SerializeField] private TMP_Text commodityTMP;
    
        [Space]
    
        [SerializeField] private Image image;

        [Space]

        [SerializeField] private GameObject commodityValueDisplay;
        [SerializeField] private GameObject imageDisplay;


        public enum TextSize
        {
            Small,
            Normal
        }
    
        private void Reset()
        {
            titleTMP.gameObject.SetActive(false);
            messageTMP.gameObject.SetActive(false);
            messageSmallTMP.gameObject.SetActive(false);
            commodityValueDisplay.SetActive(false);
            imageDisplay.SetActive(false);
        }
    
        public void Show(string contentText, TextSize size)
        {
            if (isShown) return;
        
            switch (size)
            {
                case TextSize.Small:
                    messageSmallTMP.text = contentText;
                    messageSmallTMP.gameObject.SetActive(true);
                    break;
            
                case TextSize.Normal:
                    messageTMP.text = contentText;
                    messageTMP.gameObject.SetActive(true);
                    break;
            }
        
            Show();
            isShown = true;
        }
    
        public void Show(string title, string contentText, TextSize size)
        {
            if (isShown) return;

            titleTMP.text = title;
            titleTMP.gameObject.SetActive(true);

            switch (size)
            {
                case TextSize.Small:
                    messageSmallTMP.text = contentText;
                    messageSmallTMP.gameObject.SetActive(true);
                    break;
            
                case TextSize.Normal:
                    messageTMP.text = contentText;
                    messageTMP.gameObject.SetActive(true);
                    break;
            }

            Show();
            isShown = true;
        }

        public void Show(Sprite sprite, string contentText, TextSize size)
        {
            if (isShown) return;

            image.sprite = sprite;
            imageDisplay.SetActive(true);
        
            switch (size)
            {
                case TextSize.Small:
                    messageSmallTMP.text = contentText;
                    messageSmallTMP.gameObject.SetActive(true);
                    break;
            
                case TextSize.Normal:
                    messageTMP.text = contentText;
                    messageTMP.gameObject.SetActive(true);
                    break;
            }
        
            Show();
            isShown = true;
        }
    
        public void Show(string title, Sprite sprite, string contentText, TextSize size)
        {
            if (isShown) return;

            titleTMP.text = title;
            image.sprite = sprite;
        
            titleTMP.gameObject.SetActive(true);
            imageDisplay.SetActive(true);
        
            switch (size)
            {
                case TextSize.Small:
                    messageSmallTMP.text = contentText;
                    messageSmallTMP.gameObject.SetActive(true);
                    break;
            
                case TextSize.Normal:
                    messageTMP.text = contentText;
                    messageTMP.gameObject.SetActive(true);
                    break;
            }
        
            Show();
            isShown = true;
        }
    
        public void ShowCommodityValue(int value)
        {
            if (isShown) return;

            commodityTMP.text = $"+{value}";
            commodityValueDisplay.SetActive(true);
        
            Show();
            isShown = true;
        }

        public override void Hide()
        {
            base.Hide();

            Reset();
        
            isShown = false;
        }
    }
}
