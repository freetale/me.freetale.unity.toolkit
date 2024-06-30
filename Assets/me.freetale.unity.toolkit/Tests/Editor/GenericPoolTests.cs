using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FreeTale.Unity.Toolkit.Tests.Editor
{
    public class GenericPoolTests
    {

        public class IntInstance
        {
            public int value;

            public IntInstance(int value)
            {
                this.value = value;
            }
        }

        [Test]
        public void GenericPoolTestsSimplePasses()
        {
            var pool = new GenericPool<object>();
            pool.Factory = new PoolItemFactory<object>();
            Assert.AreEqual(0, pool.Capacity);
            Assert.AreEqual(0, pool.AvaliableCount);
            var hold = pool.Get();

            Assert.AreEqual(1, pool.Capacity);
            Assert.AreEqual(0, pool.AvaliableCount);

            pool.Return(hold);

            Assert.AreEqual(1, pool.Capacity);
            Assert.AreEqual(1, pool.AvaliableCount);

            pool.PreFill(new[] { new { } });
            Assert.AreEqual(2, pool.Capacity);
            Assert.AreEqual(2, pool.AvaliableCount);
        }

        [Test]
        public void PrefillTests()
        {
            var pool = new GenericPool<IntInstance>();
            pool.Factory = new PoolItemFactory<IntInstance>();
            pool.PreFill(new List<IntInstance>()
            {
                new(1),
                new(2),
                new(3),
                new(4),
                new(5),
            });

            Assert.AreEqual(5, pool.Capacity);
            Assert.AreEqual(5, pool.AvaliableCount);
            var hold = pool.Get();
            Assert.AreEqual(1, hold.value);

            Assert.AreEqual(5, pool.Capacity);
            Assert.AreEqual(4, pool.AvaliableCount);

            pool.Return(hold);

            Assert.AreEqual(5, pool.Capacity);
            Assert.AreEqual(5, pool.AvaliableCount);
        }
    }
}
