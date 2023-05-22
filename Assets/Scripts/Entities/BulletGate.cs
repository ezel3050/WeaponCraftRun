using System;
using System.Collections.Generic;
using Components;
using TMPro;
using UnityEngine;

namespace Entities
{
    public class BulletGate : MonoBehaviour
    {
        [SerializeField] private SpriteActiveHandler gunSpriteActiveHandler;
        [SerializeField] private List<SpriteActiveHandler> starsSpriteActiveHandlers;
        [SerializeField] private MeshRenderer gateMeshRenderer;
        [SerializeField] private Material activeMaterial;
        [SerializeField] private int gateNumber;
        [SerializeField] private TextMeshPro yearAmountText;
        [SerializeField] private GameObject textObject;

        private bool _isActive;
        private int _yearAmount;

        public Action<int, bool> onPassedFromGate;

        public void Initialize(int year)
        {
            _yearAmount = year;
            yearAmountText.text = "+" + _yearAmount;
        }
        
        public void Active()
        {
            gunSpriteActiveHandler.Active();
            foreach (var starsSpriteActiveHandler in starsSpriteActiveHandlers)
            {
                starsSpriteActiveHandler.Active();
            }

            gateMeshRenderer.material = activeMaterial;
            _isActive = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Weapon")) return;
            onPassedFromGate?.Invoke(gateNumber, _isActive);
            DeActiveChosenObjects();
        }

        private void DeActiveChosenObjects()
        {
            foreach (var starsSpriteActiveHandler in starsSpriteActiveHandlers)
            {
                starsSpriteActiveHandler.gameObject.SetActive(false);
            }
            
            textObject.SetActive(false);
        }
    }
}