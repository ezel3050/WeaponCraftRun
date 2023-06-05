using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Entities
{
    public class SpeedRail : MonoBehaviour
    {
        [SerializeField] private bool isSpeedUp;
        [SerializeField] private GameObject speedUpVisual;
        [SerializeField] private GameObject speedDownVisual;

        public bool IsSpeedUp => isSpeedUp;
        private void Start()
        {
            SyncVisual();
        }

        [Button]
        public void SyncVisual()
        {
            if (isSpeedUp)
            {
                speedDownVisual.SetActive(false);
                speedUpVisual.SetActive(true);
            }
            else
            {
                speedDownVisual.SetActive(true);
                speedUpVisual.SetActive(false);
            }
        }
    }
}