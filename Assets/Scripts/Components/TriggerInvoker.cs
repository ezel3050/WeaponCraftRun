using System;
using UnityEngine;

namespace Components
{
    public class TriggerInvoker : MonoBehaviour
    {
        public Action<Collider> onTriggerEnter;
        public Action<Collider> onTriggerExit;
        public Action<Collider> onTriggerStay;

        void OnTriggerEnter(Collider other)
        {
            onTriggerEnter?.Invoke(other);
        }

        void OnTriggerExit(Collider other)
        {
            onTriggerExit?.Invoke(other);
        }

        void OnTriggerStay(Collider other)
        {
            onTriggerStay?.Invoke(other);
        }
    }
}