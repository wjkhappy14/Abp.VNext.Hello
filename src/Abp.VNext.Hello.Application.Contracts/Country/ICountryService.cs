using Abp.VNext.Hello.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface ICountryService :
          IApplicationService
    {
        Task<CountryDto> FindByNameAsync(string name);
    }
}
