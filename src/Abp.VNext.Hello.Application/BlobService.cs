using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    [RemoteService(false)]
    public class BlobService : ApplicationService, IBlobService
    {
        private readonly IBlobRepository _repository;
        private readonly ICapPublisher _capBus;
        public BlobService(IBlobRepository repository, ICapPublisher capPublisher)
        {
            _repository = repository;
            _capBus = capPublisher;
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
            _capBus.PublishAsync("Now", $"{DateTime.Now}");
            return Task.FromResult(new BlobItemDto());
        }

        public Task<int> CreateAsync(BlobItemDto input)
        {
            _capBus.PublishAsync(input.Name, new BlobItemEto()
            {
                Name = input.Name,
                Content = input.Content
            });
            return Task.FromResult(1);
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