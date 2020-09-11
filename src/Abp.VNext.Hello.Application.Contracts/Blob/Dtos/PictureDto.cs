using System.Collections;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello
{
    public class BlobItemDto : EntityDto<int>
    {

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public string Md5 { get; set; }

        public IDictionary Tags { get; set; }

        public long Length { get; set; }

        public string Name { get; set; }

    }
}
