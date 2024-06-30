using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeTale.Unity.Toolkit
{
    public static class RandomToolkit
    {
        public static Vector2 InsideRect(Rect rect)
        {
            var x = Random.Range(rect.xMin, rect.xMax);
            var y = Random.Range(rect.yMin, rect.yMax);
            return new Vector2(x, y);
        }
    }
}
