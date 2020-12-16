using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello.IdentityResources
{
    public interface IIdentityResourceService : IApplicationService
    {
        Task<List<IdentityResourceDto>> GetListByScopeNameAsync(
            string[] scopeNames
        );

        Task<List<IdentityResourceDto>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            string filter = null
        );

        Task<IdentityResourceDto> FindByNameAsync(
            string name
        );

        Task<bool> CheckNameExistAsync(
            string name,
            Guid? expectedId = null
         );
    }
}