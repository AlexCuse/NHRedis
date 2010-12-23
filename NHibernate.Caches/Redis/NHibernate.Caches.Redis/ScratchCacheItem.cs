﻿using NHibernate.Cache;

namespace NHibernate.Caches.Redis
{
    public class ScratchCacheItem
    {
        public VersionedPutParameters PutParameters
        { 
            get; set;
        }
        public object CurrentCacheValue
        {
            get; set;
        }
        public byte[] NewCacheItemRaw
        {
            get;
            set;
        }
        public ScratchCacheItem(VersionedPutParameters putParameters)
        {
            PutParameters = putParameters;
        }
    }
}
