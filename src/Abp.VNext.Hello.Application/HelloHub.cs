using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Abp.VNext.Hello
{

    [Authorize]
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

        [Authorize("Administrators")]
        public void BanUser(string userName)
        {
        }
        [Authorize("DomainRestricted")]
        public void ViewUserHistory(string username)
        {
        }
    }
}
