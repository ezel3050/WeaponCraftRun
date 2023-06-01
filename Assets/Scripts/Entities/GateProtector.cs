using System;
using UnityEngine;

namespace Entities
{
    public class GateProtector : MonoBehaviour
    {
        [SerializeField] protected float outsideProgressXPos;
        [SerializeField] protected Transform innerProgressBar;
        [SerializeField] protected GameObject wholeProgressBarObject;
        [SerializeField] protected Collider objectCollider;
        
        protected int _initObjectCount;
        protected float _health;

        public Action onShieldBroke;

        protected virtual void ShieldBroken()
        {
            onShieldBroke?.Invoke();
            objectCollider.enabled = false;
            wholeProgressBarObject.SetActive(false);
        }

        protected virtual void BulletHit(Collider other)
        {
            var bullet = other.GetComponent<Bullet>();
            bullet.BulletHit();
            _health--;
            var healthPercent = _health / _initObjectCount;
            var targetProgressXPos = outsideProgressXPos * (1 - healthPercent);
            innerProgressBar.localPosition = new Vector3(targetProgressXPos, 0, 0);
        }
    }
}