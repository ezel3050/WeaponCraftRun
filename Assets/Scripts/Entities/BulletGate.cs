using System;
using System.Collections.Generic;
using Components;
using Managers;
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
        [SerializeField] private Collider gateCollider;

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

        public void PassedFromGate()
        {
            if (_isActive)
                SoundManager.Instance.CollideWithGate();
            onPassedFromGate?.Invoke(gateNumber, _isActive);
            DeActiveChosenObjects();
        }

        public int GetValue()
        {
            return _isActive ? _yearAmount : 0;
        }

        private void DeActiveChosenObjects()
        {
            foreach (var starsSpriteActiveHandler in starsSpriteActiveHandlers)
            {
                starsSpriteActiveHandler.gameObject.SetActive(false);
            }
            
            textObject.SetActive(false);
        }

        public void DisableCollider()
        {
            gateCollider.enabled = false;
        }
    }
}