using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello.ApiResources
{
    public interface IApiResourceService : IApplicationService
    {
        Task<ApiResourceDto> FindByNameAsync(
            string name,
            bool includeDetails = true
        );

        Task<List<ApiResourceDto>> GetListByScopesAsync(
            string[] scopeNames
        );

        Task<List<ApiResourceDto>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            string filter = null
        );



        Task<bool> CheckNameExistAsync(
            string name,
            Guid? expectedId = null
        );
    }
}