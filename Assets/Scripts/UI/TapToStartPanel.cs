using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TapToStartPanel : MonoBehaviour
    {
        [SerializeField] private Button btn;

        private void Start()
        {
            btn.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            gameObject.SetActive(false);
            Managers.GameManager.StartLevel();
        }
    }
}