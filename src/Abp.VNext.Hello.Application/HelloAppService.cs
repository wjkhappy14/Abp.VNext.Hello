using Abp.VNext.Hello.Localization;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Security.Claims;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;

namespace Abp.VNext.Hello
{

   // [DebuggerStepThrough]
    public abstract class HelloAppService : ApplicationService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public IHttpContextAccessor HttpContext => LazyGetRequiredService(ref _httpContextAccessor);


        public Claim Merchant => CurrentUser.FindClaim("Merchant");


        private IDistributedEventBus _eventBus;

        public IDistributedEventBus EventBus => LazyGetRequiredService(ref _eventBus);


        public HelloAppService()
        {
            LocalizationResource = typeof(HelloResource);
        }
    }
}
