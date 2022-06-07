using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Abp.VNext.Hello
{
    public class TickPushConnectionHandler : ConnectionHandler
    {
        public TickPushConnectionHandler(ILogger<string> logger)
        {
            logger.LogInformation("MessagesConnectionHandler");
            Start();
        }
        private ConcurrentDictionary<string, ConnectionContext> Connections { get; } = new ConcurrentDictionary<string, ConnectionContext>();

        private static ConnectionMultiplexer Redis => ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            Password = "03hx5DDDivYmbkTgDlFz",
            EndPoints = {
                new IPEndPoint(IPAddress.Parse("117.50.40.186"), 6379)
            }
        });

        static ISubscriber Subscriber => Redis.GetSubscriber();

        private void Start()
        {
            Subscriber.Subscribe("CME:EC:2206", async (channel, message) => await Publish(message));
            Subscriber.Subscribe("HKEX:HSI:2205", async (channel, message) => await Publish(message));
        }
        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            Connections[connection.ConnectionId] = connection;
            HttpTransportType? transportType = connection.Features.Get<IHttpTransportFeature>()?.TransportType;
            await Publish($"{connection.ConnectionId} connected ({transportType})");
            try
            {
                while (true)
                {
                    ReadResult result = await connection.Transport.Input.ReadAsync();
                    ReadOnlySequence<byte> buffer = result.Buffer;
                    try
                    {
                        if (!buffer.IsEmpty)
                        {
                            // We can avoid the copy here but we'll deal with that later
                            string text = Encoding.UTF8.GetString(buffer.ToArray());
                            text = $"{connection.ConnectionId}: {text}";
                            await Broadcast(Encoding.UTF8.GetBytes(text));
                        }
                        else if (result.IsCompleted)
                        {
                            break;
                        }
                    }
                    finally
                    {
                        connection.Transport.Input.AdvanceTo(buffer.End);
                    }
                }
            }
            finally
            {
                Connections.Remove(connection.ConnectionId, out var xxxx);
                await Publish($"{connection.ConnectionId} disconnected ({transportType})");
            }
        }

        private Task Publish(string text) => Broadcast(Encoding.UTF8.GetBytes(text));

        private Task Broadcast(byte[] payload)
        {
            List<Task> tasks = new List<Task>(Connections.Count);
            foreach (ConnectionContext c in Connections.Values)
            {
                tasks.Add(c.Transport.Output.WriteAsync(payload).AsTask());
            }
            return Task.WhenAll(tasks);
        }
    }
}
