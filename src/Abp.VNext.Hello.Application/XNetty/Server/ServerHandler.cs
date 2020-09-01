using System;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;

namespace Abp.VNext.Hello.XNetty.Server
{
    public class ServerHandler : SimpleChannelInboundHandler<string>
    {
        private static Lazy<ServerHandler> _handler = new Lazy<ServerHandler>(() => new ServerHandler(), true);

        public static ServerHandler Handler => _handler.Value;
        public Func<IChannelHandlerContext, Task> OnMessageReceived { get; set; } = context => Task.CompletedTask;
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
            base.ChannelActive(contex);
            string msg = $"欢迎 to {0} secure chat server! { Dns.GetHostName()}\n";

            ReplyContent<string> reply = new ReplyContent<string>()
            {
                ConnectionId = $"{contex.Channel.Id}",
                Cmd = 0,
                Scope = 0,
                Message = msg
            };
            contex.WriteAndFlushAsync(reply.ToString());
            g.Add(contex.Channel);
        }


        protected override void ChannelRead0(IChannelHandlerContext contex, string msg)
        {
            RequestCommand<string> cmd = RequestCommand<string>.GetCommand(msg);
            ReplyContent<string> reply = new ReplyContent<string>()
            {
                ConnectionId = $"{contex.Channel}",
                Cmd = cmd.Cmd,
                Scope = cmd.Scope
            };
            //OnMessageReceived
            // Group.WriteAndFlushAsync(reply, new AllChannelMatcher(contex.Channel.Id));
            contex.WriteAndFlushAsync(reply.ToString());
            OnMessageReceived(contex);
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
