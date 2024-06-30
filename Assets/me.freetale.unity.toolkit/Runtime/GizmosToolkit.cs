using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeTale.Unity.Toolkit
{
    public static class GizmosToolkit
    {
        public static void DrawRect(Rect r, float z)
        {
            Gizmos.DrawLine(new Vector3(r.xMin, r.yMin, z), new Vector3(r.xMin, r.yMax, z));
            Gizmos.DrawLine(new Vector3(r.xMin, r.yMin, z), new Vector3(r.xMax, r.yMin, z));
            Gizmos.DrawLine(new Vector3(r.xMax, r.yMax, z), new Vector3(r.xMin, r.yMax, z));
            Gizmos.DrawLine(new Vector3(r.xMax, r.yMax, z), new Vector3(r.xMax, r.yMin, z));
        }
    }
}
