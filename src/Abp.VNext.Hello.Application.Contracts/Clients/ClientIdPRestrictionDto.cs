using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientIdPRestrictionDto : EntityDto
    {
        public virtual Guid ClientId { get; set; }

        public virtual string Provider { get; set; }

        protected ClientIdPRestrictionDto()
        {

        }

        public virtual bool Equals(Guid clientId, [NotNull] string provider)
        {
            return ClientId == clientId && Provider == provider;
        }

        protected internal ClientIdPRestrictionDto(Guid clientId, [NotNull] string provider)
        {
            ClientId = clientId;
            Provider = provider;
        }

       
    }
}