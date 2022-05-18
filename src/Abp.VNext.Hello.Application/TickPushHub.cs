using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Abp.VNext.Hello
{
    public class TickPushHub : AbpHub
    {
        public TickPushHub()
        {
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
            },
            ChannelPrefix = "X"
        });
        private void Start()
        {
            Subscribe("HKEX:MHI:1904");
            Subscribe("SGX:CN:1904");
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
                await Clients.All.SendAsync("responseHandler", message);
            });
        }
    }
}
