using System;
using System.Globalization;
using Enums;
using TMPro;
using UnityEngine;

namespace Entities
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private GateTypes gateType;
        [SerializeField] private TextMeshPro titleText;
        [SerializeField] private TextMeshPro coefficientText;
        [SerializeField] private TextMeshPro valueText;
        [SerializeField] private MeshRenderer gateRenderer;
        [SerializeField] private Material positiveMaterial;
        [SerializeField] private Material positiveMaterialFade;
        [SerializeField] private Material negativeMaterial;
        [SerializeField] private Material negativeMaterialFade;
        [SerializeField] private float initValue;
        [SerializeField] private float initCoefficient;

        private float _currentValue;

        public GateTypes GateTypes => gateType;
        public float CurrentValue => _currentValue;

        private void Start()
        {
            _currentValue = initValue;
            titleText.text = gateType.ToString();
            valueText.text = (_currentValue >= 0 ? "+" : "") + _currentValue;
            coefficientText.text = initCoefficient.ToString(CultureInfo.InvariantCulture);
            ChangeColorBaseOnValue();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
                BulletHit(other);
        }

        private void BulletHit(Collider other)
        {
            var bullet = other.GetComponent<Bullet>();
            bullet.BulletHit();
            _currentValue += initCoefficient;
            valueText.text = (_currentValue >= 0 ? "+" : "") + _currentValue;
            ChangeColorBaseOnValue();
        }

        private void ChangeColorBaseOnValue()
        {
            if (_currentValue >= 0)
                TurnToGreen();
            else
                TurnToRed();
        }

        private void TurnToGreen()
        {
            var materials = new Material[2];
            materials[0] = positiveMaterial;
            materials[1] = positiveMaterialFade;
            gateRenderer.materials = materials;
        }
        
        private void TurnToRed()
        {
            var materials = new Material[2];
            materials[0] = negativeMaterial;
            materials[1] = negativeMaterialFade;
            gateRenderer.materials = materials;
        }

        public void DestroyItself()
        {
            Destroy(gameObject);
        }
    }
}