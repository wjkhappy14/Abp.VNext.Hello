using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface IPictureRepository : IBasicRepository<Picture, int>
    {
        Task<List<Picture>> FindByMerchantIdAsync(Guid? tenantId);

        Task<Picture> FindByNoAsync(string serialNo);

        Task<List<Picture>> SearchAsync(Guid? tenantId, DateTime begin, DateTime end);
    }
}
