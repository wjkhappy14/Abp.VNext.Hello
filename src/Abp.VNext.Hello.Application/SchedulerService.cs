using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public class SchedulerService: ApplicationService, ISchedulerService
    {
        public SchedulerService(ISchedulerRepository schedulerRepository)
        {
            
        }
    }
}
