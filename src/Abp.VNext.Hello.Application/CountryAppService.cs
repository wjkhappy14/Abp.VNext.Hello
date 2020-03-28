using Abp.VNext.Hello.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{

    [Authorize]
    public class CountryAppService : ApplicationService, ICountryService
    {
        ICountryRepository _productRepository;

        public CountryAppService(ICountryRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CountryDto> Create(CountryDto input)
        {
            Country result = await _productRepository.InsertAsync(new Country() { });

            return ObjectMapper.Map<Country, CountryDto>(result);
        }

        public Task Delete(int id)
        {
            return _productRepository.DeleteAsync(id);
        }

      
        public Task<CountryDto> FindByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CountryDto> GetAsync(int id)
        {
            Country result = await _productRepository.GetAsync(id);
            return ObjectMapper.Map<Country, CountryDto>(result);
        }

      

        public async Task<ListResultDto<CountryDto>> GetListAsync()
        {
            List<Country> items = await _productRepository.GetListAsync();

            return new ListResultDto<CountryDto>(
                ObjectMapper.Map<List<Country>, List<CountryDto>>(items)
            );
        }

        public async Task<CountryDto> Update(int id, CountryDto input)
        {
            Country result = await _productRepository.UpdateAsync(new Country() { });

            return ObjectMapper.Map<Country, CountryDto>(result);
        }
    }
}
