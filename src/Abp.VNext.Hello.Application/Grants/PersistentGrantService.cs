using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.Grants;

namespace Abp.VNext.Hello.Grants
{
    public class PersistentGrantService : ApplicationService, IPersistentGrantService
    {
        IPersistentGrantRepository PersistentGrantRepository;
        public PersistentGrantService(IPersistentGrantRepository persistentGrantRepository, IPersistedGrantStore persistedGrantStore)
        {
            PersistentGrantRepository = persistentGrantRepository;
        }

        public async Task<PersistedGrantDto> FindByKeyAsync(
            string key
        )
        {
            PersistedGrant item = await PersistentGrantRepository.FindByKeyAsync(key);
            return ObjectMapper.Map<PersistedGrant, PersistedGrantDto>(item);
        }

        public async Task<List<PersistedGrantDto>> GetListBySubjectIdAsync(string key)
        {
            List<PersistedGrant> items = await PersistentGrantRepository.GetListBySubjectIdAsync(key);
            return ObjectMapper.Map<List<PersistedGrant>, List<PersistedGrantDto>>(items);
        }

        public async Task<List<PersistedGrantDto>> GetListByExpirationAsync(
                DateTime maxExpirationDate,
                int maxResultCount
            )
        {
            List<PersistedGrant> items = await PersistentGrantRepository.GetListByExpirationAsync(maxExpirationDate, maxResultCount);
            return ObjectMapper.Map<List<PersistedGrant>, List<PersistedGrantDto>>(items);
        }


        public Task DeleteAsync(
            string subjectId,
            string clientId,
            string type
        )
        {
            return PersistentGrantRepository.DeleteAsync(subjectId, clientId, type);
        }
    }
}