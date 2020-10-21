using Abp.VNext.Hello.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public  interface IStateProvinceAppService : IApplicationService
    {
        Task<ListResultDto<StateProvinceDto>> GetListAsync();

        Task<StateProvinceDto> GetByNameAsync(string name);



        Task<StateProvinceDto> GetAsync(int id);


        Task<StateProvinceDto> Create(StateProvinceDto input);

        Task<StateProvinceDto> Update(int id, StateProvinceDto input);

        Task Delete(int id);
    }
}
