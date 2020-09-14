using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers
{
    public class BitmaskSet
    {
        private int _bit = 0;

        public void Add(int targetIndex)
        {
            _bit = _bit | (1 << targetIndex);
        }

        public bool IsContains(int targetIndex)
        {
            int result = _bit & (1 << targetIndex);

            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Remove(int targetIndex)
        {
            _bit = _bit & ~(1 << targetIndex);
        }
    }
}