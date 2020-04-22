using System;
using System.Net;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;

namespace Abp.VNext.Hello.XNetty.Server
{
    public class ServerHandler : SimpleChannelInboundHandler<string>
    {
        private static Lazy<ServerHandler> _handler = new Lazy<ServerHandler>(() => new ServerHandler(), true);
       

        public event EventHandler<IChannelHandlerContext> OnChannelActive;
        public event EventHandler<string> OnChannelRead0;

        public static ServerHandler Handler => _handler.Value;

        private ServerHandler()
        {
           
        }
        static volatile IChannelGroup group;

        public override void ChannelActive(IChannelHandlerContext contex)
        {
            IChannelGroup g = Group;
            if (g == null)
            {
                lock (this)
                {
                    if (Group == null)
                    {
                        g = Group = new DefaultChannelGroup(contex.Executor);
                    }
                }
            }
            this.OnChannelActive(this, contex);
            contex.WriteAndFlushAsync(string.Format("欢迎 to {0} secure chat server!\n", Dns.GetHostName()));
            g.Add(contex.Channel);
        }


        protected override void ChannelRead0(IChannelHandlerContext contex, string msg)
        {
            msg = $"收到来自{ contex.Channel.RemoteAddress}的消息：{msg}";
            string response = string.Format("收到：\n", msg);

            Group.WriteAndFlushAsync(msg, new AllChannelMatcher(contex.Channel.Id));

            OnChannelRead0(this, msg);
            contex.WriteAndFlushAsync(response);

            if (string.Equals("bye", msg, StringComparison.OrdinalIgnoreCase))
            {
                contex.CloseAsync();
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext ctx) => ctx.Flush();

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine("{0}", e.StackTrace);
            ctx.CloseAsync();
        }

        public override bool IsSharable => true;

        public static IChannelGroup Group { get => group; set => group = value; }
    }

}
