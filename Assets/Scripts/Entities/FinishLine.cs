using System;
using UnityEngine;

namespace Entities
{
    public class FinishLine : MonoBehaviour
    {
        public Action onFinishLinePassed;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("WeaponPoint")) return;
            onFinishLinePassed?.Invoke();
        }
    }
}