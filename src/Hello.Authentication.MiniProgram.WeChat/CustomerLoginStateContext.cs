using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Hello.Authentication.MiniProgram.WeChat
{
    /// <summary>
    /// 用户进行自定义登录态操作时所需要的上下文信息
    /// </summary>
    public class CustomerLoginStateContext : ResultContext<WeChatMiniProgramOptions>
    {
        public CustomerLoginStateContext(
            HttpContext context,
            AuthenticationScheme scheme,
            WeChatMiniProgramOptions options,
            string openId,
            string sessionKey,
            string uniodId,
            string errCode,
            string errMsg,
            string sessionInfoKey = null) : base(context, scheme, options)
        {
            OpenId = openId;
            SessionKey = sessionKey;
            UnionId = uniodId;
            ErrCode = errCode;
            ErrMsg = errMsg;
            SessionInfoKey = sessionInfoKey;
        }

        /// <summary>
        /// 用户唯一标识
        /// 【请注意该信息的安全性,不要下发至客户端】
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 会话密钥
        /// 【请注意该信息的安全性,不要下发至客户端】
        /// </summary>
        public string SessionKey { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 微信服务端返回的密匙保存在缓存中所关联的Key。
        /// 该值需要SaveSessionKeyToCache配置为true时才有实际意义。
        /// </summary>
        public string SessionInfoKey { get; set; }
    }
}
