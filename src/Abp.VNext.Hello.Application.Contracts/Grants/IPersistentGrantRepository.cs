using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello.Grants
{
    public interface IPersistentGrantService : IApplicationService
    {
        Task<PersistedGrantDto> FindByKeyAsync(
            string key
        );

        Task<List<PersistedGrantDto>> GetListBySubjectIdAsync(
            string key
        );

        Task<List<PersistedGrantDto>> GetListByExpirationAsync(
            DateTime maxExpirationDate,
            int maxResultCount
        );

       

        Task DeleteAsync(
            string subjectId,
            string clientId,
            string type
        );
    }
}