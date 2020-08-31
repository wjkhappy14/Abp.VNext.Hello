using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    [Authorize]
    public class BlobService : ApplicationService, IBlobService
    {
        private readonly IBlobRepository _repository;
        public BlobService(IBlobRepository repository)
        {
            _repository = repository;
        }
        public async Task<ListResultDto<BlobItemDto>> SearchAsync(DateTime begin, DateTime end, IDictionary tags)
        {
            List<BlobItem> items = await _repository.SearchAsync(tags, begin, end);

            return new ListResultDto<BlobItemDto>(
                ObjectMapper.Map<List<BlobItem>, List<BlobItemDto>>(items)
            );
        }

        public Task<BlobItemDto> GetByNameAsync(string shortName)
        {
            throw new NotImplementedException();
        }

        public Task<BlobItemDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BlobItemDto> CreateAsync(BlobItemDto input)
        {
            throw new NotImplementedException();
        }

        public Task<BlobItemDto> UpdateAsync(int id, BlobItemDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<ListResultDto<BlobItemDto>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string hash)
        {
            throw new NotImplementedException();
        }



    }
}