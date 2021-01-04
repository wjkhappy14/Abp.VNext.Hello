using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Abp.VNext.Hello
{
    public class HelloHub : AbpHub
    {
        public HelloHub()
        {
            System.Diagnostics.Debug.Write("Hello");
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
