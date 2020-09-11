using System.Threading;
using System.Threading.Tasks;

namespace Abp.VNext.Hello
{
    public class ThreadService : HelloAppService
    {
        public ThreadService()
        {

        }


        public Thread CurrentThread() => Thread.CurrentThread;

        public Task<object> PoolAsync()
        {
            var threadPool = new
            {
                ThreadPool.ThreadCount,
                ThreadPool.CompletedWorkItemCount,
                ThreadPool.PendingWorkItemCount
            };
            return Task.FromResult<object>(threadPool);
        }
    }
}
