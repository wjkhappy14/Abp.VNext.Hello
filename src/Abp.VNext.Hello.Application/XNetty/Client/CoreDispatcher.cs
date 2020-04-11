using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abp.VNext.Hello.XNetty
{
    public class CoreDispatcher
    {
        private static Lazy<CoreDispatcher> _coreDispatcher = new Lazy<CoreDispatcher>(() => new CoreDispatcher(), true);

        /// <summary>
        ///[交易] 发送心跳定时器
        /// </summary>
        public Timer TradeHeartBeatTimer { get; private set; } = new Timer((x) =>
        {

        })
        {

        };

        /// <summary>
        /// 加载运行时候的动态设置参数
        /// </summary>
       // public Dictionary<string, object> Settings { get; set; } = SqLiteUtils.Settings();
        /// <summary>
        /// 止损止盈设置
        /// </summary>
        public List<Tuple<long, string, int, int>> StopProfitLossSettings { get; set; } = new List<Tuple<long, string, int, int>>();

        private readonly ConcurrentDictionary<Tuple<string, string>, Action<string>> HandlerActions = new ConcurrentDictionary<Tuple<string, string>, Action<string>>();

        public static CoreDispatcher Dispatcher { get => _coreDispatcher.Value; }

        public TimeSpan TimeDiff { get; set; }

        private CoreDispatcher()
        {
            Parallel.ForEach(Account.AccountActions, (item) => HandlerActions.TryAdd(item.Key, item.Value));

            //当网络接口的 IP 地址更改时发生。
            NetworkChange.NetworkAddressChanged += (x, y) =>
            {

            };
            //当网络的可用性更改时发生。
            NetworkChange.NetworkAvailabilityChanged += (object sender, NetworkAvailabilityEventArgs e) =>
            {
                string msg = e.IsAvailable ? "网络可用" : "网络不可用";
                //ToastNotification.Toast.ShowWarning(msg);
            };
            ClientBootstrap.Client.InitBootstrapAsync().ContinueWith(async x =>
            {
                if (x.Exception is null)
                {
                    if (x.IsCompleted)
                    {
                        await ClientBootstrap.Client.InitBootstrapAsync();
                    }
                    // await Account.ServerTimeAsync();
                }
            });
        }

      

        public void ExecAction(object message)
        {
            string json = message.ToString();
            ReplyContent<object> reply = ReplyContent<object>.GetModuleInfo(json);

            Tuple<string, string> actionKey = Tuple.Create(reply.Scope, reply.Cmd);
            Tuple<RequestCommand, ReplyObject> item = Tuple.Create<RequestCommand, ReplyObject>(null, reply);

            if (HandlerActions.ContainsKey(actionKey))
            {
                Action<string> action = HandlerActions[actionKey];
                action.Invoke(json);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <param name="type">1 行情服务器 2 交易服务器</param>
        public void ExceptionCaught(IChannelHandlerContext context, Exception exception, int type)
        {
            string msg = string.Empty;
            string ip = context.Channel.RemoteAddress.ToString();
            if (exception is SocketException)
            {
                SocketException ex = exception as SocketException;
                msg = $"{ex.Message}{ip}";
                if (type == 1)
                {
                }
                else if (type == 2)
                {
                   
                }
            }
            else
            {
                msg = $"{ip}{exception.Message}{exception.StackTrace}";
            }
            switch (type)
            {
                case 1:
                    msg += $"连接行情服务器发生异常";

                    break;
                case 2:
                    msg += $"连接交易服务器发生异常";
                    break;
            }
            throw new Exception(msg);
        }



        public Account Account => Account.Instance;

        public PushHandler PushHandler => new PushHandler(PushNotify);

        public PushNotify PushNotify => PushNotify.Instance;

        /// <summary>
        /// 重新连接
        /// </summary>
        /// <returns></returns>
        public Task ReconntectAsync()
        {
            return ClientBootstrap.Client.InitBootstrapAsync();
        }

        public Task ExceptionLog(Exception exception)
        {
            Exception context = new Exception();
            string stackTrace = string.IsNullOrEmpty(exception.StackTrace) ? string.Empty : exception.StackTrace.Replace("'", "$");
            string source = string.IsNullOrEmpty(exception.Source) ? string.Empty : exception.Source.Replace("'", "$");

            return Task.Run(() =>
            {

            });
        }
        /// <summary>
        /// 关闭所有TCP连接
        /// </summary>
        /// <param name="whenShutdown"></param>
        public Task ShutdownGracefullyAsyn(Action whenShutdown)
        {
            try
            {
                TradeHeartBeatTimer = null;
                return Task.Run(() =>
                {

                    ClientBootstrap.Client.TradeChannel.CloseAsync();
                    ClientBootstrap.Client.Group.ShutdownGracefullyAsync();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                whenShutdown.Invoke();
            }
        }
    }

}
