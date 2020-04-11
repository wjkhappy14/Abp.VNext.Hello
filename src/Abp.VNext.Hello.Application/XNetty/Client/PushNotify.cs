using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.VNext.Hello.XNetty
{
    public class PushNotify
    {
        /// <summary>
        /// 延迟初始化
        /// </summary>
        static Lazy<PushNotify> lazy = new Lazy<PushNotify>(() => new PushNotify(), true);
        public static PushNotify Instance => lazy.Value;
        private PushNotify()
        {

        }
        public delegate void EventHandler(string msg, Tuple<int, string> tuple);
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NotifyHandler;
        /// <summary>
        /// 交易服务器主动推送
        /// </summary>
        /// <param name="json"></param>
        /// <param name="tuple"></param>
        private void OnNotifyHandler(string json, Tuple<int, string> tuple)
        {
            NotifyHandler?.Invoke(json, tuple);
        }

        /// <summary>
        /// 服务端主动推送统一入口
        /// module=14 cmdId=1	资金账户更新
        /// module=14 cmdId=2	用户在它处登录
        /// module=14 cmdId=3	用户拉黑
        /// module=14 cmdId=4	交易委托推送
        /// module=14 cmdId=5	开仓成功推送
        /// module=14 cmdId=6	平仓成功推送
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tuple"></param>
        public void Notify(string json, Tuple<int, string> tuple)
        {
            OnNotifyHandler(json, tuple);
        }


    }

}
