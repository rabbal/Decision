using System;
using System.Configuration;

namespace Decision.Common.Caching
{
    public class RedisCache : CacheBase
    {
        private ConnectionMultiplexer _redisConnections;

        public RedisCache()
        {
            InitializeConnection();
        }

        private IDatabase RedisDatabase
        {
            get
            {
                if (_redisConnections == null)
                {
                    InitializeConnection();
                }
                return _redisConnections?.GetDatabase();
            }
        }

        public override bool IsCacheRunning => _redisConnections != null && _redisConnections.IsConnected;

        private void InitializeConnection()
        {
            try
            {
                _redisConnections =
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
            var endPoints = _redisConnections.GetEndPoints();
            foreach (var endPoint in endPoints)
            {
                var server = _redisConnections.GetServer(endPoint);
                server.FlushAllDatabases();
            }
        }

        protected override void DeleteByPatternProtected(string key)
        {
            throw new NotImplementedException();
        }
    }
}