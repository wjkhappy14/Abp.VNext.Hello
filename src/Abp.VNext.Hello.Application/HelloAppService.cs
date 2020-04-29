using Abp.VNext.Hello.Localization;
using System.Security.Claims;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;

namespace Abp.VNext.Hello
{
    public abstract class HelloAppService : ApplicationService
    {
        private IDistributedEventBus _eventBus;

        public Claim Merchant => CurrentUser.FindClaim("Merchant");

        public IDistributedEventBus EventBus => LazyGetRequiredService(ref _eventBus);
        protected HelloAppService()
        {
            LocalizationResource = typeof(HelloResource);
        }
    }
}
