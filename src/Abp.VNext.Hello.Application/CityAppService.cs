using Abp.VNext.Hello.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace Abp.VNext.Hello
{

    [AllowAnonymous]
    public class CityAppService : HelloAppService, ICityAppService
    {
        ICityRepository _cityRepository;

        private IDistributedCache<string> DistributedCache { get; }

        public CityAppService(ICityRepository cityRepository, IDistributedCache<string> distributedCache)
        {
            _cityRepository = cityRepository;
            DistributedCache = distributedCache;
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

        public async Task<List<CityDto>> GetPagedListAsync(PagedAndSortedResultRequestDto input)
        {
            List<City> items = await _cityRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, true);
            List<CityDto> result = ObjectMapper.Map<List<City>, List<CityDto>>(items);
            long totalCount = await _cityRepository.GetCountAsync();
            return result;
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

        public Task<PagedResultDto<CityDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new System.NotImplementedException();
        }
    }
}