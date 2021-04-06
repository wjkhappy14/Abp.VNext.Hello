using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hello.Authentication.MiniProgram.WeChat
{
    public class DefaultSessionInfoStore : IWeChatSessionInfoStore
    {
        private readonly IDistributedCache _distributedCache;
        private const string keyPrefix = "wechat-";

        public DefaultSessionInfoStore(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task RenewAsync(string key, WeChatSessionInfo sessionInfo, WeChatMiniProgramOptions currentOption)
        {
            await _distributedCache.RemoveAsync(key);

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(currentOption.CacheSlidingExpiration);
            await _distributedCache.SetAsync(key, CreateSesionBytes(sessionInfo), options);
        }

        public async Task<string> StoreAsync(WeChatSessionInfo sessionInfo, WeChatMiniProgramOptions currentOption)
        {
            var key = keyPrefix + Guid.NewGuid().ToString();

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(currentOption.CacheSlidingExpiration);

            await _distributedCache.SetAsync(key, CreateSesionBytes(sessionInfo), options);

            return key;
        }

        public async Task<WeChatSessionInfo> GetSessionInfo(string key)
        {
            var value = await _distributedCache.GetAsync(key);

            if (value == null)
                return null;

            return JsonSerializer.Deserialize<WeChatSessionInfo>(value);
        }

        private byte[] CreateSesionBytes(WeChatSessionInfo sessionInfo)
            => JsonSerializer.SerializeToUtf8Bytes(sessionInfo, typeof(WeChatSessionInfo));

    }
}
