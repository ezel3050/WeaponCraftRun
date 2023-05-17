using System;
using UnityEngine;

namespace DefaultNamespace.Components
{
    public class MaterialOffsetChanger : MonoBehaviour
    {
        [SerializeField] private Material targetMaterial;
        [SerializeField] private float speed;

        private float _counter;
        private Vector2 _tempVector2 = new Vector2();
        private void Update()
        {
            _counter += Time.deltaTime * speed;
            _tempVector2.y = -_counter;
            targetMaterial.mainTextureOffset = _tempVector2;
        }
    }
}