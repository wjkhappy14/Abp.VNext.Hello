using Abp.VNext.Hello.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    // [Authorize]
    public class StateProvinceAppService : ApplicationService, IStateProvinceAppService
    {
        IStateProvinceRepository _stateProvinceRepository;

        public StateProvinceAppService(IStateProvinceRepository stateProvinceRepository)
        {
            _stateProvinceRepository = stateProvinceRepository;
        }

        public async Task<StateProvinceDto> Create(StateProvinceDto input)
        {
            StateProvince item = await _stateProvinceRepository.InsertAsync(new StateProvince() { });

            return ObjectMapper.Map<StateProvince, StateProvinceDto>(item);
        }

        public Task Delete(int id)
        {
            return _stateProvinceRepository.DeleteAsync(id);
        }

        public async Task<StateProvinceDto> GetAsync(int id)
        {
            StateProvince item = await _stateProvinceRepository.GetAsync(id);
            return ObjectMapper.Map<StateProvince, StateProvinceDto>(item);
        }

        public async Task<StateProvinceDto> GetByNameAsync(string name)
        {
            StateProvince result = await _stateProvinceRepository.FindByNameAsync(name);

            return ObjectMapper.Map<StateProvince, StateProvinceDto>(result);
        }

        public async Task<ListResultDto<StateProvinceDto>> GetListAsync()
        {
            List<StateProvince> items = await _stateProvinceRepository.GetListAsync();

            return new ListResultDto<StateProvinceDto>(
                ObjectMapper.Map<List<StateProvince>, List<StateProvinceDto>>(items)
            );
        }

        public async Task<StateProvinceDto> Update(int id, StateProvinceDto input)
        {
            StateProvince item = ObjectMapper.Map<StateProvinceDto, StateProvince>(input);
            item = await _stateProvinceRepository.UpdateAsync(item);
            return ObjectMapper.Map<StateProvince, StateProvinceDto>(item);
        }

        public async Task<List<StateProvinceDto>> GetPagedListAsync(int skipCount, int maxResultCount = 50, string sorting = "Name")
        {
            List<StateProvince> items = await _stateProvinceRepository.GetPagedListAsync(skipCount, maxResultCount, sorting);
            return ObjectMapper.Map<List<StateProvince>, List<StateProvinceDto>>(items);
        }
    }
}
