using System;
using System.Collections.Generic;
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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Weapon")) return;
            foreach (var moneyRenderer in moneyRenderers)
            {
                moneyRenderer.enabled = false;
            }
            moneyParticle.Play();
            CurrencyHandler.IncreaseMoney(GetValue());
        }

        private int GetValue()
        {
            var randomVal = Random.Range(minRangeValue, maxRangeValue + 1);
            var finalVal = randomVal * Prefs.Income;
            var finalValInt = Mathf.CeilToInt(finalVal);
            return finalValInt;
        }
    }
}