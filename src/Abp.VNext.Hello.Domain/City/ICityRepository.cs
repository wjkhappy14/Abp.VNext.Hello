using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface ICityRepository : IBasicRepository<City, int>
    {
        Task<List<City>> FindByMerchantIdAsync(int merchantId);

        Task<City> FindByNoAsync(string serialNo);

        Task<List<City>> SearchAsync(int? merchantId, DateTime begin, DateTime end);
    }
}
