using System;
using System.Collections.Generic;
using Managers;
using Statics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class CollectingMoney : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> moneyRenderers;
        [SerializeField] private ParticleSystem moneyParticle;
        [SerializeField] private int minRangeValue;
        [SerializeField] private int maxRangeValue;

        private bool _isUsed;

        private void OnTriggerEnter(Collider other)
        {
            if (_isUsed) return;
            if (!other.CompareTag("Weapon")) return;
            _isUsed = true;
            foreach (var moneyRenderer in moneyRenderers)
            {
                moneyRenderer.enabled = false;
            }
            moneyParticle.Play();
            var position = Camera.main!.WorldToScreenPoint(transform.position);
            CurrencyHandler.IncreaseMoney(GetValue(), position, true);
        }

        private int GetValue()
        {
            var randomVal = Random.Range(minRangeValue, maxRangeValue + 1);
            var finalVal = randomVal * (1 + Prefs.IncomeLevel * 0.1f);
            var finalValInt = Mathf.CeilToInt(finalVal);
            return finalValInt;
        }
    }
}