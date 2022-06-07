using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Abp.VNext.Hello
{
    [HubRoute("/signalr-hubs/tick-push")]
    public class TickPushHub : AbpHub
    {
        ILogger<TickPushHub> _logger;
        IConfiguration config;
        public TickPushHub(ILogger<TickPushHub> logger, IConfiguration configuration)
        {
            _logger = logger;
            config = configuration;
            _logger.Log(LogLevel.Information, "Tick Push Hub");
        }

        public override Task OnConnectedAsync()
        {
           
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

      
        public async Task Send(string message)
        {
            message = $"{DateTime.Now}: {message}";
            await Clients.All
                .SendAsync("ReceiveMessage", message);
        }

        public Task Publish(string message)
        {
            return this.Clients.All.SendAsync("responseHandler", message);
        }
    }
}
