using System;
using UnityEngine;

namespace Components
{
    public class ScaleBouncer : MonoBehaviour
    {
        private float _tempTime;
        private Vector3 _defaultScale;
        private void Start()
        {
            _defaultScale = transform.localScale;
        }

        private void Update()
        {
            if(_tempTime > 0)
            {
                var increaseAmount = _defaultScale.x / 10;
                var scaleTarget = new Vector3(_defaultScale.x + increaseAmount, _defaultScale.y + increaseAmount,
                    _defaultScale.z + increaseAmount);
                transform.localScale = Vector3.Slerp(transform.localScale, scaleTarget, Time.deltaTime * 40);
                _tempTime -= Time.deltaTime;
            }
            else
            {
                _tempTime = 0;
                transform.localScale = Vector3.Slerp(transform.localScale, _defaultScale,Time.deltaTime * 40);
            }   
        }

        public void Poke()
        {
            _tempTime = 0.025f;
        }
    }
}