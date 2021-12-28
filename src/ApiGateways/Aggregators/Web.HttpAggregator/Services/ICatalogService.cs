using Web.HttpAggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.HttpAggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync(IEnumerable<int> ids);
    }
}
