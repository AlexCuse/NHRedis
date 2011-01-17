#region License

//
//  NHRedis - A cache provider for NHibernate using the .NET client
// ServiceStackRedis for Redis
// (http://code.google.com/p/servicestack/wiki/ServiceStackRedis)
//
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or (at your option) any later version.
//
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//  Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// CLOVER:OFF
//

#endregion

using System.Collections.Generic;
using log4net.Config;
using NHibernate.Cache;
using NUnit.Framework;

namespace NHibernate.Caches.Redis.Tests
{
    public class NhRedisClientNoClearFixture : NhRedisClientFixture
    {

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            XmlConfigurator.Configure();
            _props = new Dictionary<string, string> {{RedisProvider.NoClearPropertyKey, "true"}, 
                                                     {AbstractCache.ExpirationPropertyKey, "20"},
                                                    {AbstractCache.LockAcquisitionTimeoutPropertyKey, "1"},
                                                     {AbstractCache.LockTimeoutPropertyKey, "20"}
            };
            _provider = new RedisProvider();
            _provider.Start(_props);
        }
        [Test]
        public override void TestClear()
        {
        }
        [Test]
        public void TestLock()
        {

            ICache cache = _provider.BuildCache(null, new TestInMemoryQueryProvider(), CacheFactory.ReadWriteCow, _props);
            Assert.IsNotNull(cache, "no cache returned");

            string key = "key1";
    
            Assert.IsTrue(cache.Lock(key));

            //can't re-lock
            Assert.IsFalse(cache.Lock(key));
  
            cache.Unlock(key);

            //can now lock
            Assert.IsTrue(cache.Lock(key));

            //cleanup
            cache.Unlock(key);
 
        }
    }
}