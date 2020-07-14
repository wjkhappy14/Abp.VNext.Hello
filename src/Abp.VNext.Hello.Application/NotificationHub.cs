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
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Identity;

namespace Abp.VNext.Hello
{

    [Authorize]
    public class NotificationHub : AbpHub
    {
        private static List<HubCallerContext> Connections { get; } = new List<HubCallerContext>();//HubConnectionContext
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly ILookupNormalizer _lookupNormalizer;
        private static ConnectionMultiplexer Redis => RedisHelper.RedisMultiplexer();
        ILogger<string> _logger;

        private IChannelGroup ChannelGroup => ServerHandler.Group;

        public NotificationHub(IIdentityUserRepository identityUserRepository, ILookupNormalizer lookupNormalizer, ILogger<string> logger)
        {
            _logger = logger;
            _lookupNormalizer = lookupNormalizer;
            _identityUserRepository = identityUserRepository;
            Subscribe("new_order");
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
            StringValues names = Context.GetHttpContext().Request.Query["name"];
            string name = names.Count >= 1 ? names[0] : "unknown";
            Connections.Add(this.Context);

            //var targetUser =  _identityUserRepository.FindByNormalizedUserNameAsync(_lookupNormalizer.NormalizeName(name));

            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Message = "",// $"{CurrentUser.UserName}",
                Scope = "hub",
                Cmd = "connected"
            };
            return Clients.All.SendAsync("", reply);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.GetHttpContext().Request.Query["name"];
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Message = $"{name} left the chat",
                Cmd = "disconnected"
            };
            return Clients.All.SendAsync("", reply);
        }

        public Task Send(string name, string message)
        {
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "send"
            };
            return Clients.All.SendAsync("", reply);
        }

        public Task SendToOthers(string name, string message)
        {

            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "SendToOthers"
            };
            return Clients.Others.SendAsync("", reply);
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

        public Task Handler(string scope, string cmd, string message)
        {
            if (ChannelGroup != null && ChannelGroup.Count > 0)
            {
                ChannelGroup.WriteAndFlushAsync(message, ChannelMatchers.All());
            }

            string connectionId = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = scope,
                Message = message,
                Cmd = "handler"
            };
            return Clients.Caller.SendAsync("handler", reply);
        }

        public Task Login(RequestCommand request)
        {
            string connectionId = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "login"
            };
            return Clients.Caller.SendAsync("login", reply);
        }

        public Task Token(string request)
        {
            string connectionId = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "token"
            };
            return Clients.Caller.SendAsync("token", reply);
        }


        public Task SendToConnection(string connectionId, string name, string message)
        {
            string connectionIdFrom = Context.ConnectionId;
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "SendToConnection"
            };
            return Clients.Client(connectionId).SendAsync("connection", reply);
        }

    }

}
