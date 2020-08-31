using System;
using System.Collections;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface IBlobService : IApplicationService
    {
        Task<ListResultDto<BlobItemDto>> GetListAsync();

        Task<BlobItemDto> GetByNameAsync(string shortName);

        Task<BlobItemDto> GetAsync(int id);


        Task<BlobItemDto> CreateAsync(BlobItemDto input);

        Task<BlobItemDto> UpdateAsync(int id, BlobItemDto input);

        Task DeleteAsync(string  hash);

        Task<ListResultDto<BlobItemDto>> SearchAsync(DateTime begin, DateTime end, IDictionary tags);
    }
}
