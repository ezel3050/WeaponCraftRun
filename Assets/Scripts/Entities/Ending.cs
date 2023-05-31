using Statics;
using UnityEngine;

namespace Entities
{
    public class Ending : MonoBehaviour
    {
        [SerializeField] private GameObject highScorePrefab;
        [SerializeField] private Vector3 initPosForHighScore;
        [SerializeField] private float increaseValueForHighScore;

        public void CreateHighScore()
        {
            var level = Prefs.HighScore;
            if (level == 0) return;
            var highScoreClone = Instantiate(highScorePrefab, transform);
            var pos = new Vector3(initPosForHighScore.x, initPosForHighScore.y,
                initPosForHighScore.z + increaseValueForHighScore * level);
            highScoreClone.transform.localPosition = pos;
        }
    }
}