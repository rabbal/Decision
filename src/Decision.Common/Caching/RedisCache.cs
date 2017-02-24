using System;
using System.Configuration;
using StackExchange.Redis;

namespace NTierMvcFramework.Common.Caching
{
    public class RedisCache : BaseCache, IRedisCache
    {
        private ConnectionMultiplexer redisConnections;

        public RedisCache()
        {
            InitializeConnection();
        }

        private IDatabase RedisDatabase
        {
            get
            {
                if (redisConnections == null)
                {
                    InitializeConnection();
                }
                return redisConnections?.GetDatabase();
            }
        }

        public override bool IsCacheRunning => redisConnections != null && redisConnections.IsConnected;

        private void InitializeConnection()
        {
            try
            {
                redisConnections =
                    ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["CacheConnectionString"]);
            }
            catch (RedisConnectionException errorConnectionException)
            {
                throw new Exception(
                    "Error connecting the redis cache : " + errorConnectionException.Message, errorConnectionException);
            }
        }

        protected override string GetStringProtected(string key)
        {
            if (RedisDatabase == null)
            {
                return null;
            }
            var redisObject = RedisDatabase.StringGet(key);
            return redisObject.HasValue ? redisObject.ToString() : null;
        }

        protected override void SetStringProtected(string key, string objectToCache, TimeSpan? expiry = null)
        {
            RedisDatabase?.StringSet(key, objectToCache, expiry);
        }

        protected override void DeleteProtected(string key)
        {
            RedisDatabase?.KeyDelete(key);
        }

        protected override void FlushAllProtected()
        {
            if (RedisDatabase == null)
            {
                return;
            }
            var endPoints = redisConnections.GetEndPoints();
            foreach (var endPoint in endPoints)
            {
                var server = redisConnections.GetServer(endPoint);
                server.FlushAllDatabases();
            }
        }

        protected override void DeleteByPatternProtected(string key)
        {
            throw new NotImplementedException();
        }
    }
}