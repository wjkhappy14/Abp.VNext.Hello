using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{
    public class BlobController : AbpController
    {
        private IBlobService FileService { get; }
        public BlobController(IBlobService fileService)
        {
            FileService = fileService;
        }

        [HttpPost]
        [Route("blob/upload")]
        public async Task<JsonResult> Upload(IFormFile file)
        {
            Stream stream = file.OpenReadStream();
            ReadResult data = default;
            PipeReader reader = PipeReader.Create(stream);
            do
            {
                data = await reader.ReadAsync();
            }
            while (data.IsCompleted);
            ReadOnlySequence<byte> blob = data.Buffer;

            BlobItemDto item = new BlobItemDto();
            await FileService.CreateAsync(item);

            return Json(new { Data = blob.ToArray() });
        }
    }
}
