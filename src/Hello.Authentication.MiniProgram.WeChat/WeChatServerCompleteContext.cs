using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Hello.Authentication.MiniProgram.WeChat
{
    /// <summary>
    /// 包含了微信服务器返回的信息以及当前验证处理的上下文信息
    /// </summary>
    public class WeChatServerCompletedContext : ResultContext<WeChatMiniProgramOptions>
    {
        public WeChatServerCompletedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            WeChatMiniProgramOptions options,
            string openId,
            string sessionKey,
            string uniodId,
            string errCode,
            string errMsg) : base(context, scheme, options)
        {
            OpenId = openId;
            SessionKey = sessionKey;
            UnionId = uniodId;
            ErrCode = errCode;
            ErrMsg = errMsg;
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
    }
}
