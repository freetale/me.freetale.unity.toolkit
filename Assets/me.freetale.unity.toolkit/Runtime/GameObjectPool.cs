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

        public void Awake()
        {
            Factory = new GameObjectFactory(Prototype, Parent);
            PreFill(PreInstance);
        }

    }
}
