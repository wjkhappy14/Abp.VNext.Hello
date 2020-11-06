using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;

namespace Abp.VNext.Hello
{
    public class BackgroundJobService : ApplicationService
    {
        private IBackgroundJobRepository BackgroundJobRepository;

        public BackgroundJobService(IBackgroundJobRepository backgroundJobRepository)
        {
            BackgroundJobRepository = backgroundJobRepository;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(BackgroundJobRecord entity, bool autoSave = false)
        {
            throw new NotImplementedException();
        }

    

        public Task<BackgroundJobRecord> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync()
        {
            throw new NotImplementedException();
        }

    
        public Task<List<BackgroundJobRecord>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting)
        {
            throw new NotImplementedException();
        }

        public Task<List<BackgroundJobRecord>> GetWaitingListAsync(int maxResultCount)
        {
            return BackgroundJobRepository.GetWaitingListAsync(maxResultCount);
        }

        public Task<BackgroundJobRecord> InsertAsync(BackgroundJobRecord entity)
        {
            return BackgroundJobRepository.InsertAsync(entity);
        }

        public Task<BackgroundJobRecord> UpdateAsync(BackgroundJobRecord entity)
        {
            return BackgroundJobRepository.UpdateAsync(entity);
        }
    }
}
