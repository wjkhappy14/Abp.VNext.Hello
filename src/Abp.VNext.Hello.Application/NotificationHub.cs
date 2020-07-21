using Abp.VNext.Hello.XNetty;
using Abp.VNext.Hello.XNetty.Server;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.SettingManagement;

namespace Abp.VNext.Hello
{
    [AllowAnonymous]
    public class NotificationHub : AbpHub
    {
        private static List<HubCallerContext> Connections { get; } = new List<HubCallerContext>();//HubConnectionContext
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly ISettingManager _settingManager;
        private readonly ILookupNormalizer _lookupNormalizer;
        private readonly IDistributedEventBus _eventBus;
        private static ConnectionMultiplexer Redis => RedisHelper.RedisMultiplexer();

        readonly ILogger<string> _logger;

        private IChannelGroup ChannelGroup => ServerHandler.Group;

        public NotificationHub(IIdentityUserRepository identityUserRepository,
            ILookupNormalizer lookupNormalizer,
            ILogger<string> logger,
            IDistributedEventBus eventBus,
            ISettingManager settingManager)
        {
            _logger = logger;
            _eventBus = eventBus;
            _settingManager = settingManager;
            _lookupNormalizer = lookupNormalizer;
            _identityUserRepository = identityUserRepository;
            // Subscribe("new_order");
            ServerHandler.Handler.OnChannelActive += Handler_OnChannelActive;
            ServerHandler.Handler.OnChannelRead0 += (e, s) =>
            {
                ReplyContent<object> reply = new ReplyContent<object>
                {
                    Scope = "tcp",
                    Cmd = "push",
                    Message = s
                };
                Clients.All.SendAsync(reply.ToString());
            };
        }
        private void Subscribe(string channel)
        {
            ISubscriber sub = Redis.GetSubscriber();
            sub.Subscribe(channel, (channel, message) =>
            {
                long id = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                System.Diagnostics.Debug.WriteLine(message);

                ReplyContent<object> reply = new ReplyContent<object>
                {
                };
                reply.Result = new { };
                Clients.All.SendAsync("", reply);
            });
        }
        private void Handler_OnChannelActive(object sender, IChannelHandlerContext e)
        {
            ReplyContent<object> reply = new ReplyContent<object>
            {
                Cmd = "active",
                Scope = "tcp",
            };
            Clients.All.SendAsync("", reply);
        }

        public override Task OnConnectedAsync()
        {
            StringValues id = Context.GetHttpContext().Request.Query["id"];
            Connections.Add(this.Context);

            //var targetUser =  _identityUserRepository.FindByNormalizedUserNameAsync(_lookupNormalizer.NormalizeName(name));

            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Message = "连接成功",// $"{CurrentUser.UserName}",
                Scope = id,
                Cmd = "connecte"
            };
            return Clients.All.SendAsync("connected", reply);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.GetHttpContext().Request.Query["id"];
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Message = $"{id} left the chat",
                Cmd = "disconnected"
            };
            return Clients.All.SendAsync("disconnected", reply);
        }

        public Task Send(string name, string message)
        {
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "send"
            };
            return Clients.All.SendAsync("send", reply);
        }

        public Task SendToOthers(string name, string message)
        {

            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "SendToOthers"
            };
            return Clients.Others.SendAsync("sendToOthers", reply);
        }

        public Task SendToGroup(string groupName, string name, string message)
        {
            string connectionId = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "SendToGroup"
            };
            return Clients.Group(groupName).SendAsync("toGroup", reply);
        }

        public Task SendToOthersInGroup(string groupName, string name, string message)
        {
            string connectionId = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "SendToOthersInGroup"
            };
            return Clients.OthersInGroup(groupName).SendAsync("toOthers", reply);
        }

        public async Task JoinGroup(string groupName, string name)
        {
            string connectionId = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "JoinGroup"
            };
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("join", reply);
        }

        public async Task LeaveGroup(string groupName, string name)
        {
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "LeaveGroup"
            };

            await Clients.Group(groupName).SendAsync("leave", reply);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public Task Handler(RequestCommand request)
        {
            //if (ChannelGroup != null && ChannelGroup.Count > 0)
            //{
            //    ChannelGroup.WriteAndFlushAsync(request, ChannelMatchers.All());
            //}
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = request.Scope,
                Message = request.Message,
                Cmd = request.Cmd
            };
            _eventBus.PublishAsync(reply);
            return Clients.Caller.SendAsync("receive", reply);
        }

        public Task Login(RequestCommand request)
        {
            string connectionId = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = connectionId,
                Scope = "hub",
                Cmd = "login"
            };
            return Clients.Caller.SendAsync("receive", reply);
        }

        public Task Token(string request)
        {
            string connectionId = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = connectionId,
                Scope = "hub",
                Cmd = "token"
            };
            return Clients.Caller.SendAsync("receive", reply);
        }

        public Task SendTo(string connectionId, string message)
        {
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Message = message,
                Cmd = "sendTo"
            };
            return Clients.Client(connectionId).SendAsync("sendTo", reply);
        }

        /// <summary>
        /// 流式传输
        /// 有些场景中，服务器返回的数据量较大，等待时间较长，客户端不得不等待服务器返回所有数据后，再进行相应的操作。这时使用流式传输，
        /// 将服务器数据碎片化，当每个数据碎片读取完成之后，就只传输完成的部分，而不需要等待所有数据都读取完成。
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public async Task<ChannelReader<int>> DelayCounterAsync(int delay)
        {
            Channel<int> channel = Channel.CreateUnbounded<int>();
            await WriteItems(channel.Writer, 20, delay);
            return channel.Reader;
        }

        private async Task<bool> WriteItems(ChannelWriter<int> writer, int count, int delay)
        {
            for (var i = 0; i < count; i++)
            {
                await writer.WriteAsync(i);
                await Task.Delay(delay);
            }
            return writer.TryComplete();
        }
    }
}
