using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.VNext.Hello.XNetty
{
    /// <summary>
    /// 处理服务器主动推送的数据
    /// </summary>
    public class PushHandler
    {
        public static Tuple<int, string> Login { get => Tuple.Create(1, ""); }
        public Dictionary<Tuple<int, string>, Action<string>> PushActions { get; } = new Dictionary<Tuple<int, string>, Action<string>>();

        public PushNotify PushNotify { get; }
        public PushHandler(PushNotify pushNotify)
        {
            PushNotify = pushNotify;
            // PushActions.Add(Login, OnLoginOnSomewhere);
        }

        /// <summary>
        /// 账号在别的地方登录推送通知
        /// </summary>
        /// <param name="json"></param>
        private void OnLoginOnSomewhere(string json)
        {
            PushNotify.Notify(json, Login);
        }
        /// <summary>
        /// 资金账户更新
        /// </summary>
        private void OnCapitalAccount(string json)
        {
        }
        /// <summary>
        /// 用户拉黑
        /// </summary>
        /// <param name="json"></param>
        private void OnBlock(string json)
        {
        }
        /// <summary>
        /// 交易委托结果
        /// </summary>
        /// <param name="json"></param>
        private void OnDelegateOrder(string json)
        {
            PushNotify.Notify(json, Login);
        }
        /// <summary>
        /// 开仓结果
        /// </summary>
        /// <param name="json"></param>
        private void OnOpen(string json)
        {
            PushNotify.Notify(json, Login);
        }
        /// <summary>
        /// 平仓结果
        /// </summary>
        /// <param name="json"></param>
        private void OnUnwind(string json)
        {
            PushNotify.Notify(json, Login);
        }
        /// <summary>
        /// 合约更新
        /// </summary>
        /// <param name="obj"></param>
        private void OnContractsUpdate(string json)
        {
            PushNotify.Notify(json, Login);
        }
        public void OnPushMessage(string json)
        {
            PushNotify.Notify(json, Login);
        }
    }

}
