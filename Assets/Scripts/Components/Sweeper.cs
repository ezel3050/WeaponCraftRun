using System;
using UnityEngine;

namespace Components
{
    public class Sweeper : MonoBehaviour
    {
        [SerializeField] private Vector3 maxPos;
        [SerializeField] private Vector3 minPos;
        [SerializeField] private float time;

        private bool _isMoveRight;
        private float _tempTime;

        private void Update()
        {
            _tempTime += Time.deltaTime / time;
            transform.localPosition = Vector3.LerpUnclamped(transform.localPosition, _isMoveRight ? maxPos : minPos, _tempTime);
            if (Vector3.Distance(transform.localPosition, maxPos) < 0.1f)
            {
                _isMoveRight = false;
                _tempTime = 0;
            }

            if (Vector3.Distance(transform.localPosition, minPos) < 0.1f)
            {
                _isMoveRight = true;
                _tempTime = 0;
            }
        }
    }
}