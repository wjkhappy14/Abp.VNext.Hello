using Abp.VNext.Hello.Dtos;
using DotNetCore.CAP;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;

namespace Abp.VNext.Hello
{

    [UnitOfWork]
    public class CountryAppService : ApplicationService, ICountryService
    {
        ICountryRepository _countryRepository;

        public CountryAppService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<CountryDto> Create(CountryDto input)
        {
            Country result = await _countryRepository.InsertAsync(new Country() { });

            return ObjectMapper.Map<Country, CountryDto>(result);
        }

        public Task Delete(int id)
        {
            return _countryRepository.DeleteAsync(id);
        }


        public Task<CountryDto> FindByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CountryDto> GetAsync(int id)
        {
            Country result = await _countryRepository.GetAsync(id);
            return ObjectMapper.Map<Country, CountryDto>(result);
        }
        [CapSubscribe("sample.rabbitmq.sqlserver")]
        public async Task<List<CountryDto>> GetListAsync()
        {
            List<Country> items = await _countryRepository.GetListAsync();
            return ObjectMapper.Map<List<Country>, List<CountryDto>>(items);
        }

        public Task<IList<CountryDto>> Search(string keyword)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CountryDto> Update(int id, CountryDto input)
        {
            Country result = await _countryRepository.UpdateAsync(new Country() { });

            return ObjectMapper.Map<Country, CountryDto>(result);
        }
    }
}
