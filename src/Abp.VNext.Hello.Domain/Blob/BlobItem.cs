using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.VNext.Hello
{
    public class BlobItem : FullAuditedAggregateRoot<string>
    {
        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public string Md5 { get; set; }

        public string Tags { get; set; }

        public long Length { get; set; }

        public string Name { get; set; }

        public Guid TenantId { get; set; }

    }

}
