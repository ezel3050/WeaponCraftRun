using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace DefaultNamespace
{
    public class wwww : MonoBehaviour
    {
        public List<EndingBlock> EndingBlocks = new List<EndingBlock>();

        public void SetValue(int x)
        {
            foreach (var endingBlock in EndingBlocks)
            {
                endingBlock.SetValue(x);
            }
        }
    }
}