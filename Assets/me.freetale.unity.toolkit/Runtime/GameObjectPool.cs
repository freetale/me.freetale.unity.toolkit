using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FreeTale.Unity.Toolkit
{
    public class GameObjectFactory : IPoolItemFactory<GameObject>
    {
        public GameObject Prototype;
        public Transform Parent;

        public GameObjectFactory(GameObject prototype, Transform parent)
        {
            Prototype = prototype;
            Parent = parent;
        }

        public GameObject CreateInstance()
        {
            return UnityEngine.Object.Instantiate(Prototype, Parent);
        }
    }

    [Serializable]
    public class GameObjectPool : GenericPool<GameObject>
    {
        public GameObject[] PreInstance;

        public GameObject Prototype;
        public Transform Parent;

        public void Initialize()
        {
            Factory = new GameObjectFactory(Prototype, Parent);
            PreFill(PreInstance);
        }

        /// <summary>
        /// allow prefill instance, allow to have faster load time of entries scene
        /// </summary>
        public void PreFill()
        {
            for (int i = 0; i < PreInstance.Length; i++)
            {
                if (PreInstance[i] != null)
                {
                    continue;
                }
                PreInstance[i] = Factory.CreateInstance();
            }
        }
        public T GetWithComponent<T>() where T : Component
        {
            GameObject obj = Get();
            return obj.GetComponent<T>();
        }
    }
}
