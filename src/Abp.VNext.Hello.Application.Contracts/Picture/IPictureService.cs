using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface IPictureService : IApplicationService
    {
        Task<ListResultDto<PictureDto>> GetListAsync();

        Task<PictureDto> GetByNameAsync(string shortName);

        Task<PictureDto> GetAsync(int id);


        Task<PictureDto> Create(PictureDto input);

        Task<PictureDto> Update(int id, PictureDto input);

        Task Delete(int id);

        Task<ListResultDto<PictureDto>> SearchAsync(DateTime begin, DateTime end, int payStatus);
    }
}
