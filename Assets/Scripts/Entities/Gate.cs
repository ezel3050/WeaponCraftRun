using System;
using System.Globalization;
using Components;
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
        [SerializeField] private MeshRenderer gate2Renderer;
        [SerializeField] private Material positiveMaterial;
        [SerializeField] private Material positiveMaterialFade;
        [SerializeField] private Material negativeMaterial;
        [SerializeField] private Material negativeMaterialFade;
        [SerializeField] private GameObject visual1;
        [SerializeField] private GameObject visual2;
        [SerializeField] private GameObject dualGunVisual;
        [SerializeField] private GameObject moneyVisual;
        [SerializeField] private RectTransform valueTextSizeForMoneyTransform;
        [SerializeField] private ScaleBouncer scaleBouncer;
        [SerializeField] private GateProtector protector;
        [SerializeField] private BoxCollider gateCollider;
        [SerializeField] private float initValue;
        [SerializeField] private float initCoefficient;
        [SerializeField] private float limit;
        [SerializeField] private bool hasLimit;

        private float _currentValue;

        public GateTypes GateTypes => gateType;
        public float CurrentValue => _currentValue;

        private void Start()
        {
            if (protector)
            {
                gateCollider.enabled = false;
                protector.onShieldBroke += ShieldBroken;
            }
            _currentValue = initValue;
            titleText.text = gateType.ToString();
            valueText.text = (_currentValue >= 0 ? "+" : "") + _currentValue;
            coefficientText.text = initCoefficient.ToString(CultureInfo.InvariantCulture);
            ChangeColorBaseOnValue();
            ChangeVisualBaseOnType();
        }

        private void ShieldBroken()
        {
            gateCollider.enabled = true;
        }

        private void ChangeVisualBaseOnType()
        {
            if (gateType is GateTypes.Money or GateTypes.DUALWEAPON)
            {
                visual1.SetActive(false);
                visual2.SetActive(true);
                switch (gateType)
                {
                    case GateTypes.DUALWEAPON:
                        dualGunVisual.SetActive(true);
                        coefficientText.gameObject.SetActive(false);
                        valueText.gameObject.SetActive(false);
                        break;
                    case GateTypes.Money:
                        moneyVisual.SetActive(true);
                        coefficientText.gameObject.SetActive(false);
                        titleText.gameObject.SetActive(false);
                        valueText.rectTransform.sizeDelta = valueTextSizeForMoneyTransform.sizeDelta;
                        valueText.rectTransform.anchoredPosition = valueTextSizeForMoneyTransform.anchoredPosition;
                        break;
                }
            }
            else
            {
                visual1.SetActive(true);
                visual2.SetActive(false);
            }
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
            scaleBouncer.Poke();
            if (hasLimit)
                if (_currentValue >= limit) return;
            _currentValue += initCoefficient;
            valueText.text = (_currentValue >= 0 ? "+" : "") + _currentValue;
            ChangeColorBaseOnValue();
        }

        private void ChangeColorBaseOnValue()
        {
            if (initCoefficient < 0)
                coefficientText.color = Color.red;
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
            gate2Renderer.materials = materials;
        }
        
        private void TurnToRed()
        {
            var materials = new Material[2];
            materials[0] = negativeMaterial;
            materials[1] = negativeMaterialFade;
            gateRenderer.materials = materials;
            gate2Renderer.materials = materials;
        }

        public void DestroyItself()
        {
            Destroy(gameObject);
        }
    }
}