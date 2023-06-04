using System;
using Managers;
using Statics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MagazinePanel : MonoBehaviour
    {
        [SerializeField] private Image currentIcon;
        [SerializeField] private Image currentBorder;
        [SerializeField] private Image nextIcon;
        [SerializeField] private Image nextBorder;
        [SerializeField] private TextMeshProUGUI currentLevel;
        [SerializeField] private TextMeshProUGUI nextLevel;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Button videoBtn;
        [SerializeField] private Button skipBtn;

        public Action<bool> onPanelClosed;
        private void Start()
        {
            var level = Prefs.MagazineLevel;
            var models = ContentManager.Instance.GetTwoSideMagazineModels(level);
            currentIcon.sprite = models[0].MagazineSprite;
            currentBorder.sprite = models[0].BorderSprite;
            nextIcon.sprite = models[1].MagazineSprite;
            nextBorder.sprite = models[1].BorderSprite;
            currentLevel.text = "Level " + models[0].Level;
            nextLevel.text = "Level " + models[1].Level;
            descriptionText.text = "Upgrade Capacity from " + models[0].MagazineCapacity + " to " +
                                   models[1].MagazineCapacity;
            videoBtn.onClick.AddListener(VideoButtonClicked);
            skipBtn.onClick.AddListener(SkipButtonClicked);
            Invoke("ShowSkipButton", 3f);
        }

        private void SkipButtonClicked()
        {
            onPanelClosed?.Invoke(false);
        }

        private void VideoButtonClicked()
        {
            
        }

        private void ShowSkipButton()
        {
            skipBtn.gameObject.SetActive(true);
        }
    }
}