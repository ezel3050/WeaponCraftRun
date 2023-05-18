using UnityEngine;

namespace DefaultNamespace.Entities
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform shootingSpot;
        [SerializeField] private ParticleSystem shootingParticle;
    }
}