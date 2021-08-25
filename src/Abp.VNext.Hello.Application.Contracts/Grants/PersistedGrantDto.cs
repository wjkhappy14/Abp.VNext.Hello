using System;
using Volo.Abp.Domain.Entities;

namespace Abp.VNext.Hello.Grants
{
    public class PersistedGrantDto 
    {
        public virtual string Key { get; set; }

        public virtual string Type { get; set; }

        public virtual string SubjectId { get; set; }

        public virtual string ClientId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual string Data { get; set; }

        protected PersistedGrantDto()
        {
            
        }

        public PersistedGrantDto(Guid id)
        {
           
        }
    }
}