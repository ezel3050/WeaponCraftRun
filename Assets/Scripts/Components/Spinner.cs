using System;
using UnityEngine;

namespace Components
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField] private Transform spinTransform;
        [SerializeField] private float speed;
        [SerializeField] private bool isX;
        [SerializeField] private bool isY;
        [SerializeField] private bool isZ;

        private Vector3 _temp;

        private void Start()
        {
            _temp = Vector3.zero;
        }

        private void Update()
        {
            if (isX)
                _temp.x += Time.deltaTime * speed;
            if (isY)
                _temp.y += Time.deltaTime * speed;
            if (isZ)
                _temp.z += Time.deltaTime * speed;
            
            spinTransform.localRotation = Quaternion.Euler(_temp);
        }
    }
}