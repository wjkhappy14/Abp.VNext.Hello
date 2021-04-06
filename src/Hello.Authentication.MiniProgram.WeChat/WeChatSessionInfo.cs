using System;

namespace Hello.Authentication.MiniProgram.WeChat
{
    /// <summary>
    /// 微信服务端所返回的密匙信息.
    /// </summary>
    public class WeChatSessionInfo
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 会话密钥
        /// </summary>
        public string SessionKey { get; set; }

        public WeChatSessionInfo()
        {
        }

        public WeChatSessionInfo(string openId, string sessionKey)
        {
            if (string.IsNullOrWhiteSpace(openId))
                throw new ArgumentException($"{nameof(openId)} 不能为空!");

            if (string.IsNullOrWhiteSpace(sessionKey))
                throw new ArgumentException($"{nameof(sessionKey)} 不能为空!");

            OpenId = openId;
            SessionKey = sessionKey;
        }
    }
}
