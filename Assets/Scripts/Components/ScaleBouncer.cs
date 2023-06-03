using System;
using UnityEngine;

namespace Components
{
    public class ScaleBouncer : MonoBehaviour
    {
        [SerializeField] private Transform objectToBounce;
        private float _tempTime;
        private Vector3 _defaultScale;
        private void Start()
        {
            _defaultScale = objectToBounce.localScale;
        }

        private void Update()
        {
            if(_tempTime > 0)
            {
                var increaseAmount = _defaultScale.x / 10;
                var scaleTarget = new Vector3(_defaultScale.x + increaseAmount, _defaultScale.y + increaseAmount,
                    _defaultScale.z + increaseAmount);
                objectToBounce.localScale = Vector3.Slerp(objectToBounce.localScale, scaleTarget, Time.deltaTime * 40);
                _tempTime -= Time.deltaTime;
            }
            else
            {
                _tempTime = 0;
                objectToBounce.localScale = Vector3.Slerp(objectToBounce.localScale, _defaultScale,Time.deltaTime * 40);
            }   
        }

        public void Poke()
        {
            _tempTime = 0.025f;
        }
    }
}