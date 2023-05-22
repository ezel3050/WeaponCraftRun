using UnityEngine;

namespace Components
{
    public class SpriteActiveHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite deActiveSprite;

        public void Active()
        {
            renderer.sprite = activeSprite;
        }
        
        public void DeActive()
        {
            renderer.sprite = deActiveSprite;
        }
    }
}