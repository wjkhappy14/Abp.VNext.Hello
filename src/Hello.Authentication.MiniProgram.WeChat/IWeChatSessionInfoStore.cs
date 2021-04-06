using System.Threading.Tasks;

namespace Hello.Authentication.MiniProgram.WeChat
{
    /// <summary>
    /// 保存微信服务端所返回的Sessionkey等信息到缓存的操作接口.
    /// </summary>
    public interface IWeChatSessionInfoStore
    {
        /// <summary>
        /// 保存<see cref="WeChatSessionInfo"/>,并且返回所关联的Key。
        /// </summary>
        /// <param name="sessionInfo"><see cref="WeChatSessionInfo"/></param>
        /// <param name="currentOption">当前的微信验证配置信息</param>
        /// <returns>与该seesionInfo所关联的Key信息</returns>
        Task<string> StoreAsync(WeChatSessionInfo sessionInfo, WeChatMiniProgramOptions currentOption);

        /// <summary>
        /// 刷新当前Key的所对应的信息。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sessionInfo"><see cref="WeChatSessionInfo"/></param>
        /// <param name="currentOption">当前的微信验证配置信息</param>
        Task RenewAsync(string key, WeChatSessionInfo sessionInfo, WeChatMiniProgramOptions currentOption);

        /// <summary>
        /// 根据Key来移除缓存中的结果。
        /// </summary>
        /// <param name="key"></param>
        Task RemoveAsync(string key);

        /// <summary>
        /// 根据Key来获取对应的<see cref="WeChatSessionInfo"/>。
        /// </summary>
        /// <param name="key"></param>
        Task<WeChatSessionInfo> GetSessionInfo(string key);
    }
}
