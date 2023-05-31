using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> particleSystems;
        [SerializeField] private bool isPlayParticle;
        
        public Action onFinishLinePassed;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("WeaponPoint")) return;
            if (isPlayParticle)
                foreach (var particle in particleSystems)
                {
                    particle.Play();
                }
            onFinishLinePassed?.Invoke();
        }
    }
}