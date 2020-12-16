using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Abp.VNext.Hello.IdentityResources
{
    public class IdentityResourceService : ApplicationService, IIdentityResourceService
    {
        IIdentityResourceRepository IdentityResourceRepository { get; }

        public IdentityResourceService(IIdentityResourceRepository identityResourceRepository)
        {
            IdentityResourceRepository = identityResourceRepository;
        }

        public async Task<List<IdentityResourceDto>> GetListByScopeNameAsync(string[] scopeNames)
        {
            List<IdentityResource> items = await IdentityResourceRepository.GetListByScopeNameAsync(scopeNames);
            return ObjectMapper.Map<List<IdentityResource>, List<IdentityResourceDto>>(items);
        }

        public async Task<List<IdentityResourceDto>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            string filter = null
        )
        {
            List<IdentityResource> items = await IdentityResourceRepository.GetListAsync(sorting, skipCount, maxResultCount, filter);
            return ObjectMapper.Map<List<IdentityResource>, List<IdentityResourceDto>>(items);
        }

        public async Task<IdentityResourceDto> FindByNameAsync(string name)
        {
            IdentityResource item = await IdentityResourceRepository.FindByNameAsync(name, true);
            return ObjectMapper.Map<IdentityResource, IdentityResourceDto>(item);
        }

        public Task<bool> CheckNameExistAsync(
             string name,
             Guid? expectedId = null
          )
        {
            return IdentityResourceRepository.CheckNameExistAsync(name, expectedId);
        }
    }
}