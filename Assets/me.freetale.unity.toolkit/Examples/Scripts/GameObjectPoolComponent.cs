using FreeTale.Unity.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeTale.Unity.Toolkit.Examples
{
    public class GameObjectPoolComponent : MonoBehaviour
    {
        public GameObjectPool Pool;

        private void Reset()
        {
            Pool.Parent = transform;
        }

        [ContextMenu("AutoFill")]
        public void AutoFill()
        {
            Pool.Initialize();
            Pool.PreFill();
        }
    }
}