using System;
using UnityEngine;

namespace Entities
{
    public class GateProtector : MonoBehaviour
    {
        public Action onShieldBroke;

        protected virtual void ShieldBroken()
        {
            onShieldBroke?.Invoke();
        }
    }
}