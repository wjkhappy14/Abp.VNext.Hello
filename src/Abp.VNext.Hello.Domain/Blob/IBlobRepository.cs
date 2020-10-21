using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface IBlobRepository : IBasicRepository<BlobItem, string>
    {
        Task<BlobItem> FindByNoAsync(string hash);

        Task<List<BlobItem>> SearchAsync(IDictionary tags, DateTime begin, DateTime end);
    }
}
