using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface IPictureRepository : IBasicRepository<Picture, int>
    {
        Task<List<Picture>> FindByMerchantIdAsync(int merchantId);

        Task<Picture> FindByNoAsync(string serialNo);

        Task<List<Picture>> SearchAsync(int? merchantId, DateTime begin, DateTime end);
    }
}
