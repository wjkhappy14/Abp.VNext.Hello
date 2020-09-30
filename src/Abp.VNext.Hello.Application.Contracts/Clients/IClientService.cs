using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello.Clients
{
    public interface IClientService : IApplicationService
    {
        Task<ClientDto> FindByCliendIdAsync(
            [NotNull] string clientId
        );

        Task<List<ClientDto>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            string filter = null
        );

        Task<List<string>> GetAllDistinctAllowedCorsOriginsAsync();

        Task<bool> CheckClientIdExistAsync(
            string clientId,
            Guid? expectedId = null
        );
    }
}