using AutoMapper.Configuration;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Net;
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
            Start();
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        private static ConnectionMultiplexer Redis => ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            Password = "03hx5DDDivYmbkTgDlFz",
            EndPoints = {
                new IPEndPoint(IPAddress.Parse("117.50.40.186"), 6379)
            }
        });
        private void Start()
        {
            Subscribe("CME:EC:2206");
            Subscribe("HKEX:HSI:2205");
        }
        private void Subscribe(string channelName)
        {
            ISubscriber sub = Redis.GetSubscriber();
            sub.Subscribe(channelName, async (channel, message) =>
            {
                long id = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                string item = message.ToString();
                string[] values = item.Split(',');
                string timestamp = values[0];
                string qlastprice = values[1];
                string qlastqty = values[2];
                _logger.Log(LogLevel.Information, message);
                if (Clients != null)
                {
                    await Clients.All.SendAsync("responseHandler", message);
                }
            });
        }
    }
}
