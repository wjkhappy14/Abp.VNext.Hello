using Abp.VNext.Hello.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface ICityAppService :
        ICrudAppService<
            CityDto,
            int,
            PagedAndSortedResultRequestDto,
            CityDto,
            CityDto>
    {
        Task<IList<CityDto>> Search(string keyword);
    }
}