using System;
using System.Collections.Generic;
using Statics;
using UnityEngine;

namespace Entities
{
    public class RowBlocks : MonoBehaviour
    {
        [SerializeField] private List<EndingBlock> blocks;
        [SerializeField] private int level;
        
        private void Start()
        {
            foreach (var block in blocks)
            {
                block.onExploded += BlockExploded;
            }
        }

        private void BlockExploded()
        {
            if (Prefs.HighScore < level)
                Prefs.HighScore = level;
        }
    }
}