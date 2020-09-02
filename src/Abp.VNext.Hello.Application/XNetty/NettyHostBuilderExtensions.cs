using Abp.VNext.Hello.XNetty.Server;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Abp.VNext.Hello.XNetty
{
    public static class NettyHostBuilderExtensions
    {
        public static IHostBuilder UseNettyAsync(this IHostBuilder builder)
        {
            Task.Run(async () =>
            {
                Console.WriteLine($"TCP(Netty) Running on Port:443");
                await XServerBootstrap.RunServerAsync(IPAddress.Any, 444);
            });
            return builder;
        }
    }
}
