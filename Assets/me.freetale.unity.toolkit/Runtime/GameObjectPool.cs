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

#if UNITY_EDITOR
    public class GameObjectPrefabFactory : IPoolItemFactory<GameObject>
    {
        public GameObject Prototype;
        public Transform Parent;

        public GameObjectPrefabFactory(GameObject prototype, Transform parent)
        {
            Prototype = prototype;
            Parent = parent;
        }

        public GameObject CreateInstance()
        {
            return UnityEditor.PrefabUtility.InstantiatePrefab(Prototype, Parent) as GameObject;
        }
    }
#endif

    [Serializable]
    public class GameObjectPool : GenericPool<GameObject>
    {
        public GameObject[] PreInstance;

        public GameObject Prototype;
        public Transform Parent;

        public void Initialize()
        {
            Factory = new GameObjectFactory(Prototype, Parent);
        }

        /// <summary>
        /// allow prefill instance, allow to have faster load time of entries scene
        /// </summary>
        public void PreFill()
        {
#if UNITY_EDITOR
            IPoolItemFactory<GameObject> factory;
            if (!Application.isPlaying && !Prototype.scene.isLoaded) // is prefab
            {
                factory = new GameObjectPrefabFactory(Prototype, Parent);
            }
            else
            {
                factory = Factory;
            }
#else
            var factory = Factory;
#endif
            for (int i = 0; i < PreInstance.Length; i++)
            {
                if (PreInstance[i] != null)
                {
                    continue;
                }
                PreInstance[i] = factory.CreateInstance();
            }
        }

        public T GetWithComponent<T>() where T : Component
        {
            GameObject obj = Get();
            return obj.GetComponent<T>();
        }
    }
}
