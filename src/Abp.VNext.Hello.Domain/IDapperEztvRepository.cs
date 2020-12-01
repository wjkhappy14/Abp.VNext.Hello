using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.VNext.Hello
{
  public  interface IDapperEztvRepository
    {

        Task<int> BulkInsertAsync(IList<EztvItem> items);
        Task<IEnumerable<EztvItem>> GetPagesAsync(int page = 1, int limit = 100);

    }
}
