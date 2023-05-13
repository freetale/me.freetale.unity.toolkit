using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FreeTale.Unity.Toolkit
{
    public  interface IPoolItemFactory<T>
    {
        public T CreateInstance();
    }
    public class PoolItemFactory<T> : IPoolItemFactory<T>
    {
        public T CreateInstance()
        {
            return default;
        }
    }

    [Serializable]
    public class GenericPoolException : Exception
    {
        public GenericPoolException() { }
        public GenericPoolException(string message) : base(message) { }
        public GenericPoolException(string message, Exception inner) : base(message, inner) { }
        protected GenericPoolException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class GenericPool<T>
    {   
        protected List<T> Pool = new List<T>();

        protected Queue<T> Avaliable = new Queue<T>();

        public int Capacity => Pool.Count;

        public int AvaliableCount => Avaliable.Count;

        public IPoolItemFactory<T> Factory;

        public void PreFill(IEnumerable<T> values)
        {
            Pool.AddRange(values);
            foreach (T item in values) {
                Avaliable.Enqueue(item);
            }
        }


        public T Get()
        {
            if (Avaliable.Count > 0)
            {
                return Avaliable.Dequeue();
            }
            var instance = Factory.CreateInstance();
            Pool.Add(instance);
            return instance;
        }

        public void Return(T item)
        {
            if (!Pool.Contains(item))
            {
                throw new GenericPoolException("item not belong to this pool");
            }
            if (Avaliable.Contains(item))
            {
                throw new GenericPoolException("duplicate return exception");
            }
            Avaliable.Enqueue(item);
        }
    }
}
