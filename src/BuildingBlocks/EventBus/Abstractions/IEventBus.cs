using System.Threading.Tasks;
using Abp.VNext.Hello.BuildingBlocks.EventBus.Events;

namespace Abp.VNext.Hello.BuildingBlocks.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event)
            where TIntegrationEvent : IntegrationEvent;
    }
}
