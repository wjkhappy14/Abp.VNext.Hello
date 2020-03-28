using Abp.VNext.Hello.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{

     [Authorize]
    public class CityAppService :
        CrudAppService<City, CityDto, int, PagedAndSortedResultRequestDto,
            CityDto, CityDto>,
        ICityAppService
    {
        public CityAppService(IRepository<City, int> repository)
            : base(repository)
        {

        }
    }
}