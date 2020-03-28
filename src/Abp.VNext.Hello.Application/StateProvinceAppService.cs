using Abp.VNext.Hello.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    [Authorize]
    public class StateProvinceAppService : ApplicationService, IStateProvinceAppService
    {
        IStateProvinceRepository _channelRepository;

        public StateProvinceAppService(IStateProvinceRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<StateProvinceDto> Create(StateProvince input)
        {
            StateProvince item = await _channelRepository.InsertAsync(new StateProvince() { });

            return ObjectMapper.Map<StateProvince, StateProvinceDto>(item);
        }

        public Task<StateProvinceDto> Create(StateProvinceDto input)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            return _channelRepository.DeleteAsync(id);
        }

      

        public async Task<StateProvinceDto> GetAsync(int id)
        {
            StateProvince item = await _channelRepository.GetAsync(id);
            return ObjectMapper.Map<StateProvince, StateProvinceDto>(item);
        }

        public async Task<StateProvinceDto> GetByNameAsync(string name)
        {
            StateProvince result = await _channelRepository.FindByNameAsync(name);

            return ObjectMapper.Map<StateProvince, StateProvinceDto>(result);
        }

        public async Task<ListResultDto<StateProvinceDto>> GetListAsync()
        {
            string email = CurrentUser.Email;

            List<StateProvince> items = await _channelRepository.GetListAsync();

            return new ListResultDto<StateProvinceDto>(
                ObjectMapper.Map<List<StateProvince>, List<StateProvinceDto>>(items)
            );
        }

        public async Task<StateProvinceDto> Update(int id, StateProvinceDto input)
        {
            StateProvince item = await _channelRepository.UpdateAsync(new StateProvince() { });

            return ObjectMapper.Map<StateProvince, StateProvinceDto>(item);
        }
    }
}
