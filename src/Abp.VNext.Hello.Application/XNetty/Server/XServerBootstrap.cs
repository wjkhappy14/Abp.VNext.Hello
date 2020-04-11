using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;

namespace Abp.VNext.Hello.XNetty.Server
{
    public class XServerBootstrap
    {
        public static async Task RunServerAsync()
        {
            MultithreadEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
            MultithreadEventLoopGroup workerGroup = new MultithreadEventLoopGroup();

            StringEncoder STRING_ENCODER = new StringEncoder();
            StringDecoder STRING_DECODER = new StringDecoder();
            ServerHandler SERVER_HANDLER =  ServerHandler.Handler;


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


                        pipeline.AddLast(new DelimiterBasedFrameDecoder(8192, Delimiters.LineDelimiter()));
                        pipeline.AddLast(STRING_ENCODER, STRING_DECODER, SERVER_HANDLER);
                    }));

                IChannel bootstrapChannel = await bootstrap.BindAsync(666);

                // await bootstrapChannel.CloseAsync();
            }
            finally
            {
                // Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
            }
        }
    }
}
