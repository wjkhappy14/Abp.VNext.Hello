using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace Hello.Authentication.MiniProgram.WeChat
{
    /// <summary>
    /// Options for the WeChat MiniProgram authentication handler.
    /// </summary>
    public class WeChatMiniProgramOptions : RemoteAuthenticationOptions
    {
        /// <summary>
        /// 小程序 appId
        /// 【请注意该信息的安全性,避免下发至客户端】
        /// </summary>
        public string WeChatAppId { get; set; }

        /// <summary>
        /// 小程序 appSecret
        /// 【请注意该信息的安全性,避免下发至客户端】
        /// </summary>
        public string WeChatSecret { get; set; }

        /// <summary>
        /// 授权类型，该值为:authorization_code.
        /// </summary>
        public string WeChatGrantTtype { get; } = "authorization_code";

        /// <summary>
        /// 登录url中,携带小程序客户端获取到code的参数名.
        /// 默认为:"code". 
        /// 
        /// <para>
        ///     该值需要配合CallbackPath参数使用.假如CallbackPath为"signin-wechat"
        ///     则"https://yourhostaddress/signin-wechat?code=xxx"为验证地址,而"xxx"则会被传递至微信服务器进行验证.
        /// </para>
        /// </summary>
        public string WeChatJsCodeQueryString { get; set; } = "code";

        /// <summary>
        /// 根据微信服务器返回的会话密匙进行执行自定义登录态操作。
        /// 比如颁发Jwt Token，缓存OpenId，重定向至action等操作。
        /// </summary>
        public Func<CustomerLoginStateContext, Task> CustomerLoginState { get; set; }

        /// <summary>
        /// 是否要保存微信服务端所返回的OpenId和SessionKey到缓存中。
        /// 
        /// <para>
        /// 当该值为true时，验证时从DI容器中获取<see cref="IWeChatSessionInfoStore"/>服务来进行保存。
        /// 默认的保存方案为：<see cref="DefaultSessionInfoStore"/>。
        /// 该方案使用了<see cref="IDistributedCache"/>来作为缓存，所以请在使用时确保已经添加了IDistributedCache的实现方案。
        /// </para>
        /// </summary>
        public bool SaveSessionKeyToCache { get; set; }

        /// <summary>
        /// 缓存过期的时间。
        /// 默认值为：1天。
        /// </summary>
        public TimeSpan CacheSlidingExpiration { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        /// Gets or sets the <see cref="WeChatEvents"/> used to handle authentication events.
        /// </summary>
        public new WeChatEvents Events
        {
            get => (WeChatEvents)base.Events;
            set => base.Events = value;
        }

        public WeChatMiniProgramOptions()
        {
            CallbackPath = new PathString("/signin-wechat");
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            Events = new WeChatEvents();
        }

        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(WeChatAppId))
                throw new ArgumentException($"WeChatAppId 不能为空.");

            if (string.IsNullOrEmpty(WeChatSecret))
                throw new ArgumentException($"WeChatSecret 不能为空.");
        }
    }
}
