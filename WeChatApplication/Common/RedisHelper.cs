using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Common
{
    public class RedisHelper<T>where T:new()
    {
        private static ConnectionMultiplexer _redis;

        public RedisHelper()
        {
            if(_redis == null || _redis.IsConnected)
            {
                _redis = ConnectionMultiplexer.Connect("localhost");
            }
        }

        public T GetObj(string key)
        {
            try
            {
                string value = _redis.GetDatabase().StringGet(key);
                var obj = JsonConvert.DeserializeObject<T>(value);
                return obj;
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }

        public bool SetObj(string key,object obj, TimeSpan time)
        {
            try
            {
                if (obj != null)
                {
                    string value = JsonConvert.SerializeObject(obj);
                    return _redis.GetDatabase().StringSet(key, value,time);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
