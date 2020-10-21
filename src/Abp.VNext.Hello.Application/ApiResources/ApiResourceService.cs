using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.ApiResources;

namespace Abp.VNext.Hello.ApiResources
{
    public class ApiResourceService : ApplicationService, IApiResourceService
    {

        IApiResourceRepository ApiResourceRepository { get; }

        public ApiResourceService(IApiResourceRepository apiResourceRepository)
        {
            ApiResourceRepository = apiResourceRepository;
        }

        public Task<ApiResourceDto> FindByNameAsync(
            string name,
            bool includeDetails = true
        )
        {
            return null;
        }

        public async Task<List<ApiResourceDto>> GetListByScopesAsync(
            string[] scopeNames
        )
        {
            List<ApiResource> items = await ApiResourceRepository.GetListByScopesAsync(scopeNames, true);
            return ObjectMapper.Map<List<ApiResource>, List<ApiResourceDto>>(items);
        }

        public async Task<List<ApiResourceDto>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount=50,
            string filter = null
        )
        {
            List<ApiResource> items = await ApiResourceRepository.GetListAsync(sorting, skipCount, maxResultCount, filter, true);
            return ObjectMapper.Map<List<ApiResource>, List<ApiResourceDto>>(items);
        }

        public async Task<bool> CheckNameExistAsync(
            string name,
            Guid? expectedId = null
        ) => await ApiResourceRepository.CheckNameExistAsync(name, expectedId).ConfigureAwait(false);
    }
}