using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{
    public class BlobController : AbpController
    {
        private IBlobService BlobService { get; }
        public BlobController(IBlobService fileService)
        {
            BlobService = fileService;
        }


        [HttpGet]
        [Route("blob/now")]
        public async Task<JsonResult> Now(int id = 0)
        {
            BlobItemDto item = await BlobService.GetAsync(id);
            return Json(item);
        }
        [RequestSizeLimit(100000000)]
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
                reader.AdvanceTo(data.Buffer.Start, data.Buffer.End);
            }
            while (data.IsCompleted);
            ReadOnlySequence<byte> blob = data.Buffer;

            BlobItemDto item = new BlobItemDto
            {
                Content = blob.ToArray(),
                Length = file.Length,
                Name = file.FileName,
                ContentType = file.ContentType,
                Id = Thread.CurrentThread.ManagedThreadId,
                Md5 = $"{GetHashCode()}",
            };
            await BlobService.CreateAsync(item);
            return Json(item);
        }
    }
}
