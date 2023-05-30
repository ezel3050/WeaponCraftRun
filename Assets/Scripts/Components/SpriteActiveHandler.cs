using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public class SpriteActiveHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer objectRenderer;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite deActiveSprite;

        public void Active()
        {
            objectRenderer.sprite = activeSprite;
        }
        
        public void DeActive()
        {
            objectRenderer.sprite = deActiveSprite;
        }
    }
}