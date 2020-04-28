using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    [Authorize]
    public class PictureService : ApplicationService, IPictureService
    {
        private readonly IPictureRepository _repository;
        public PictureService(IPictureRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListResultDto<PictureDto>> SearchAsync(DateTime begin, DateTime end, int payStatus)
        {
            List<Picture> items = await _repository.SearchAsync(0, begin, end);

            return new ListResultDto<PictureDto>(
                ObjectMapper.Map<List<Picture>, List<PictureDto>>(items)
            );
        }

        public Task<PictureDto> GetByNameAsync(string shortName)
        {
            throw new NotImplementedException();
        }

        public Task<PictureDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PictureDto> Create(PictureDto input)
        {
            throw new NotImplementedException();
        }

        public Task<PictureDto> Update(int id, PictureDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Task<ListResultDto<PictureDto>> GetListAsync()
        {
            throw new NotImplementedException();
        }
    }
}