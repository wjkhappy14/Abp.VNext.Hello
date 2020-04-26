using Abp.VNext.Hello.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;

namespace Abp.VNext.Hello
{
    public abstract class HelloAppService : ApplicationService
    {

        private IDistributedEventBus _eventBus;
        public IDistributedEventBus EventBus => LazyGetRequiredService(ref _eventBus);
        protected HelloAppService()
        {
            LocalizationResource = typeof(HelloResource);
        }
    }
}
