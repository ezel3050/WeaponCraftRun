using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public class Sweeper : MonoBehaviour
    {
        [SerializeField] private Vector3 maxPos;
        [SerializeField] private Vector3 minPos;
        [SerializeField] private float speed;
        [SerializeField] private bool isZMinusOnly;
        [SerializeField] private bool isZPlusOnly;
        [SerializeField] private bool isXOnly;
        [SerializeField] private bool isZigZag;
        [SerializeField] private bool isZigZagReverse;
        [SerializeField] private float speedX;
        [SerializeField] private float speedZ;
        [SerializeField] private bool isMoveRight;
        private float _tempTime;
        private Vector3 _targetPos;
        private Transform _currentTransform;

        private void Start()
        {
            _currentTransform = transform;
            _targetPos = _currentTransform.localPosition;
        }

        private void Update()
        {
            if (isXOnly)
            {
                if (isMoveRight)
                    _targetPos.x += Time.deltaTime * speed;
                else 
                    _targetPos.x -= Time.deltaTime * speed;

                var localPosition = _currentTransform.localPosition;
                _targetPos.y = localPosition.y;
                _targetPos.z = localPosition.z;

                transform.localPosition = _targetPos;

                if (Vector3.Distance(transform.localPosition, maxPos) < 0.1f)
                {
                    isMoveRight = false;
                }

                if (Vector3.Distance(transform.localPosition, minPos) < 0.1f)
                {
                    isMoveRight = true;
                }
            }

            if (isZPlusOnly || isZMinusOnly)
            {
                if (isZPlusOnly)
                    _targetPos.z += Time.deltaTime * speed;
                if (isZMinusOnly)
                    _targetPos.z -= Time.deltaTime * speed;
                
                var localPosition = _currentTransform.localPosition;
                _targetPos.y = localPosition.y;
                _targetPos.x = localPosition.x;
                
                transform.localPosition = _targetPos;
            }

            if (isZigZag)
            {
                if (isZigZagReverse)
                    _targetPos.z -= Time.deltaTime * speedZ;
                else 
                    _targetPos.z += Time.deltaTime * speedZ;
                
                if (isMoveRight)
                    _targetPos.x += Time.deltaTime * speedX;
                else 
                    _targetPos.x -= Time.deltaTime * speedX;
                
                _targetPos.y = _currentTransform.localPosition.y;
                
                transform.localPosition = _targetPos;

                if (_targetPos.x - maxPos.x > 0f && isMoveRight)
                {
                    isMoveRight = false;
                }

                if (_targetPos.x - minPos.x < 0f && !isMoveRight)
                {
                    isMoveRight = true;
                }
            }
        }
    }
}