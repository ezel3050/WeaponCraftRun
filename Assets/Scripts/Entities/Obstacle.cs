using UnityEngine;

namespace Entities
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] protected bool isIgnoreHit;

        protected bool isHit;

        public void GotHit()
        {
            isHit = true;
        }

        public bool IsObstacleEffective()
        {
            if (!isHit)
            {
                return true;
            }
            return !isIgnoreHit;
        }
    }
}