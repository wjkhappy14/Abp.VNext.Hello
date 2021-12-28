using System.Threading.Tasks;
using Web.HttpAggregator.Models;

namespace Web.HttpAggregator.Services
{
    public interface IBasketService
    {
        Task UpdateAsync(BasketData currentBasket, string accessToken);
    }
}
