using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abp.VNext.Hello.XNetty
{
    public class Account
    {
        public static Tuple<string, string> Login { get => Tuple.Create("AA", "BB"); }
        /// <summary>
        /// 延迟初始化
        /// </summary>
        static Lazy<Account> lazy = new Lazy<Account>(() => new Account(), true);
        public string SessionToken => "";
        public static Account Instance => lazy.Value;
        public Dictionary<Tuple<string, string>, Action<string>> AccountActions { get; } = new Dictionary<Tuple<string, string>, Action<string>>();

        public string HeartBeatNow { get; private set; } = string.Empty;
        /// <summary>
        /// 图形验证码
        /// </summary>

        private Account()
        {
            Register();
        }
        /// <summary>
        /// 注册命令类型跟回调
        /// </summary>
        private void Register()
        {
            AccountActions.Add(Login, OnLogin);
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task LoginAsync(string account, string password)
        {
            return Task.FromResult("");

        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="password">密码</param>
        /// <param name="verificationCode">验证码</param>
        /// <param name="orgCode">组织代码</param>
        /// <returns></returns>
        public Task RegisterAsync(string phone, string password, string verificationCode, string orgCode)
        {
            return Task.Run(() => { });
        }


        /// <summary>
        /// 心跳
        /// </summary>
        /// <returns></returns>
        public Task HeartBeatAsync()
        {
            return Task.Run(() => { });
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task LogOutAsync()
        {
            return Task.Run(() => { });
        }


        /// <summary>
        /// 登录结果
        /// </summary>
        /// <param name="json"></param>
        private void OnLogin(string json)
        {
            OnCallBackEventHandler(json, Login);
        }

        /// <summary>
        /// 心跳应答
        /// </summary>
        /// <param name="json"></param>
        private void OnHeartBeat(string json)
        {

        }
        /// <summary>
        /// 与服务器时间差
        /// </summary>
        /// <param name="json"></param>
        private void OnServerTime(string json)
        {
            ReplyContent<long> replyObject = ReplyContent<long>.GetReplyContent(json);
            long clientTime = replyObject.ClientTime;
            CoreDispatcher.Dispatcher.TimeDiff = TimeSpan.FromMilliseconds(clientTime - replyObject.Result);
        }

        #region 回调事件
        /// <summary>
        /// 处理回调
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        public delegate void DelCallBack(string json, Tuple<string, string> type);
        public event DelCallBack CallBackEventHandler;
        private void OnCallBackEventHandler(string json, Tuple<string, string> type) => CallBackEventHandler?.Invoke(json, type);
        #endregion
        public void SaveAuthToken(string authToken)
        {
            if (authToken is null)
            {
                throw new ArgumentNullException(nameof(authToken));
            }
            //  SqLiteUtils.AddSetting("Phone", authToken.Phone, "登录手机号码");

        }
    }

}
