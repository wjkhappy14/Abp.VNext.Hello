using Abp.VNext.Hello.XNetty;
using Abp.VNext.Hello.XNetty.Server;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.VNext.Hello
{

    public class NotificationHub : DynamicHub
    {
        private static List<HubCallerContext> Connections { get; } = new List<HubCallerContext>();//HubConnectionContext

        private static ConnectionMultiplexer Redis = RedisHelper.RedisMultiplexer();
        ILogger<string> _logger;

        private IChannelGroup ChannelGroup => ServerHandler.Group;

        public NotificationHub(ILogger<string> logger)
        {
            _logger = logger;

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
                Clients.All.Send(reply);
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
                Clients.All.Send(reply);
            });
        }
        private void Handler_OnChannelActive(object sender, IChannelHandlerContext e)
        {
            ReplyContent<object> reply = new ReplyContent<object>
            {
                Cmd = "active",
                Scope = "tcp",
            };
            Clients.All.Send(reply);
        }

        public override Task OnConnectedAsync()
        {
            StringValues names = Context.GetHttpContext().Request.Query["name"];
            string name = names.Count >= 1 ? names[0] : "unknown";
            Connections.Add(this.Context);


            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Message = $"{name} join the chat",
                Scope = "hub",
                Cmd = "connected"
            };
            return Clients.All.Send(reply);
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
            return Clients.All.Send(reply);
        }

        public Task Send(string name, string message)
        {
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "send"
            };
            return Clients.All.Send(reply);
        }

        public Task SendToOthers(string name, string message)
        {

            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "SendToOthers"
            };
            return Clients.Others.Send(reply);
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
            return Clients.Group(groupName).Send(reply);
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
            return Clients.OthersInGroup(groupName).Send(reply);
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

            await Clients.Group(groupName).Send(reply);
        }

        public async Task LeaveGroup(string groupName, string name)
        {
            ReplyContent<object> reply = new ReplyContent<object>
            {
                ConnectionId = Context.ConnectionId,
                Scope = "hub",
                Cmd = "LeaveGroup"
            };

            await Clients.Group(groupName).Send(reply);

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
            return Clients.Caller.Send(reply);
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
            return Clients.Caller.Send(reply);
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
            return Clients.Caller.Send(reply);
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
            return Clients.Client(connectionId).Send(reply);
        }

    }

}
