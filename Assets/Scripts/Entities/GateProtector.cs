using System;
using Enums;
using Managers;
using UnityEngine;

namespace Entities
{
    public class GateProtector : Obstacle
    {
        [SerializeField] protected float outsideProgressXPos;
        [SerializeField] protected Transform innerProgressBar;
        [SerializeField] protected GameObject wholeProgressBarObject;
        [SerializeField] protected Collider objectCollider;
        [SerializeField] protected ProtectorType protectorType;
        
        protected float _initObjectCount;
        protected float _health;
        protected SoundManager _soundManager;

        public Action onShieldBroke;

        protected virtual void Start()
        {
            _soundManager = SoundManager.Instance;
        }

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