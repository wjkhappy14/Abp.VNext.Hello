using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface ICountryRepository : IBasicRepository<Country, int>
    {

        Task<Country> GetByNameAsync(string name);

    }
}
