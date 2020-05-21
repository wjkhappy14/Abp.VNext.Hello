using Abp.VNext.Hello.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{

    // [Authorize]
    public class CityAppService : HelloAppService, ICityAppService
    {
        ICityRepository _cityRepository;

        public CityAppService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public Task<CityDto> CreateAsync(CityDto input)
        {
            throw new System.NotImplementedException();
        }

        [Authorize(HelloPermissions.City.Delete)]
        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CityDto> GetAsync(int id)
        {
            City item = await _cityRepository.GetAsync(id);
            return ObjectMapper.Map<City, CityDto>(item);
        }

        public async Task<PagedResultDto<CityDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            List<City> items = await _cityRepository.GetListAsync(includeDetails: true);

            List<CityDto> result = ObjectMapper.Map<List<City>, List<CityDto>>(items);

            long totalCount = await _cityRepository.GetCountAsync();

            return new PagedResultDto<CityDto>(
                totalCount,
                result
                );
        }

        [Authorize(HelloPermissions.City.Search)]
        public async Task<IList<CityDto>> Search(string keyword)
        {
            List<City> items = await _cityRepository.SearchAsync(keyword).ToListAsync();
            return ObjectMapper.Map<IList<City>, IList<CityDto>>(items);
        }

        [Authorize(HelloPermissions.City.Update)]
        public Task<CityDto> UpdateAsync(int id, CityDto input)
        {
            throw new System.NotImplementedException();
        }
    }
}