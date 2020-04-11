using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace Abp.VNext.Hello.XNetty
{

    /// <summary>
    /// 交易服务
    /// </summary>
    public class ClientHandler : SimpleChannelInboundHandler<string>
    {
        public static ClientBootstrap ClientBootstrap => ClientBootstrap.Client;

        private Func<IChannelHandlerContext, string> whenChannelActive;
        public ClientHandler(Func<IChannelHandlerContext, string> func)
        {
            whenChannelActive = func;
        }
        public override void ChannelActive(IChannelHandlerContext context)
        {
            whenChannelActive.Invoke(context);
            if (CoreDispatcher.Dispatcher.TradeHeartBeatTimer != null)
            {
                // CoreDispatcher.Dispatcher.Account.LoginByTokenAsync();
               
            }

        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        protected override void ChannelRead0(IChannelHandlerContext ctx, string message)
        {
            System.Diagnostics.Debug.WriteLine(message.ToString());

        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            context.WriteAndFlushAsync(message);
            CoreDispatcher.Dispatcher.ExecAction(message);
        }

        /// <summary>
        /// 连接服务器发生异常
        /// 连接服务器发生异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) => CoreDispatcher.Dispatcher.ExceptionCaught(context, exception, 2);

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);
            if (CoreDispatcher.Dispatcher.TradeHeartBeatTimer != null)
            {

                ClientBootstrap.OnChannelInactiveHandler(context);
            }
            //FeedbackService.Instance.PostActiveLogAsync("TradeClientHandler", "ChannelInactive");
        }
        public override Task DisconnectAsync(IChannelHandlerContext context)
        {
            return base.DisconnectAsync(context);
        }
    }

}
