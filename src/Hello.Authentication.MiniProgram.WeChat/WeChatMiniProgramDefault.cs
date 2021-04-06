namespace Hello.Authentication.MiniProgram.WeChat
{
    // <summary>
    /// Default values for we chat mini program authentication
    /// </summary>
    public class WeChatMiniProgramDefault
    {
        public const string AuthenticationScheme = "MiniProgam,WeChat";

        public static readonly string DisplayName = "WeChatMiniProgram";

        /// <summary>
        /// 微信小程序服务端验证的url
        /// https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/login/auth.code2Session.html
        /// </summary>
        public static readonly string AuthorizationEndpoint = "https://api.weixin.qq.com/sns/jscode2session";
    }
}
