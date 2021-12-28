using System.Threading.Tasks;
using Abp.VNext.Hello.BuildingBlocks.EventBus.Events;

namespace Abp.VNext.Hello.BuildingBlocks.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler 
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
