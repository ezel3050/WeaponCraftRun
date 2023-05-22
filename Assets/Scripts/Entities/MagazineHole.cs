using Models;
using UnityEngine;

namespace Entities
{
    public class MagazineHole : MonoBehaviour
    {
        [SerializeField] private MeshRenderer outerMeshRenderer;
        [SerializeField] private MeshRenderer innerMeshRenderer;
        [SerializeField] private Rigidbody rb;

        private bool _isFilled;

        public bool IsFilled => _isFilled;

        public void Initialize(WeaponModel model)
        {
            outerMeshRenderer.material = model.OuterBulletMaterial;
            innerMeshRenderer.gameObject.SetActive(true);
            innerMeshRenderer.material = model.InnerBulletMaterial;
            _isFilled = true;
        }

        public void ActiveGravity()
        {
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}