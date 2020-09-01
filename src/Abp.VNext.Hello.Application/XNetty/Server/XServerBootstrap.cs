using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using System.Text;
using System.Net;

namespace Abp.VNext.Hello.XNetty.Server
{
    public class XServerBootstrap
    {
        public static async Task RunServerAsync(IPAddress ip, int port)
        {
            MultithreadEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
            MultithreadEventLoopGroup workerGroup = new MultithreadEventLoopGroup();

            try
            {
                ServerBootstrap bootstrap = new ServerBootstrap();
                bootstrap
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    .Handler(new LoggingHandler(LogLevel.INFO))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;

                        // pipeline.AddLast(new LoggingHandler());
                        // pipeline.AddLast("framing-Encoder", new LengthFieldPrepender(4));
                        // pipeline.AddLast("framing-Decoder", new LengthFieldBasedFrameDecoder(int.MaxValue, 0, 4, 0, 4));

                        pipeline.AddLast(new StringEncoder(Encoding.UTF8));
                        pipeline.AddLast(new StringDecoder(Encoding.UTF8));

                        pipeline.AddLast(ServerHandler.Handler);
                    }));

                IChannel bootstrapChannel = await bootstrap.BindAsync(ip, port);

                // await bootstrapChannel.CloseAsync();
            }
            finally
            {
                // Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
            }
        }
    }
}
