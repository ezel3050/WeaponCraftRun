using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class GateBullet : MonoBehaviour
    {
        public void PlayWinSequence(Vector3 firstPos)
        {
            var sq = DOTween.Sequence();
            sq.Append(transform.DOLocalMove(firstPos, 0.3f));
            var secondPos = new Vector3(4, -4, firstPos.z);
            sq.Append(transform.DOLocalMove(secondPos, 0.3f));
            sq.Join(transform.DOScale(Vector3.zero, 0.3f));
        }

        public void PlayLoseSequence()
        {
            transform.DOLocalMoveY(-4, 0.5f);
        }
    }
}