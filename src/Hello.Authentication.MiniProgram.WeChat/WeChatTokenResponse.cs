using System;
using System.Text.Json;

namespace Hello.Authentication.MiniProgram.WeChat
{
    /// <summary>
    /// 微信小程序登录凭证校验后所返回的结果。
    /// https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/login/auth.code2Session.html
    /// </summary>
    public class WeChatTokenResponse : IDisposable
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 会话密钥
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

        public Exception Error { get; set; }

        public JsonDocument Response { get; set; }

        private WeChatTokenResponse(JsonDocument response)
        {
            Response = response;
            var root = response.RootElement;
            OpenId = root.GetString("openid");
            SessionKey = root.GetString("session_key");
            UnionId = root.GetString("unionid");
            ErrCode = root.GetString("errcode");
            ErrMsg = root.GetString("errmsg");
        }

        private WeChatTokenResponse(Exception error)
        {
            Error = error;
        }

        public static WeChatTokenResponse Success(JsonDocument response)
        {
            return new WeChatTokenResponse(response);
        }

        public static WeChatTokenResponse Failed(Exception error)
        {
            return new WeChatTokenResponse(error);
        }

        public void Dispose()
        {
            Response?.Dispose();
        }
    }

    public static class JsonDocumentAuthExtensions
    {
        public static string GetString(this JsonElement element, string key)
        {
            if (element.TryGetProperty(key, out var property) && property.ValueKind != JsonValueKind.Null)
            {
                return property.ToString();
            }

            return null;
        }
    }
}
