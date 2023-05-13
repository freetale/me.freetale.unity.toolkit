using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FreeTale.Unity.Toolkit.Tests.Editor
{
    public class GenericPoolTests
    {
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
    }
}
