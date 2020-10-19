using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientGrantTypeDto : EntityDto
    {
        public virtual Guid ClientId { get; protected set; }

        public virtual string GrantType { get; protected set; }

        protected ClientGrantTypeDto()
        {

        }

        public virtual bool Equals(Guid clientId, [NotNull] string grantType)
        {
            return ClientId == clientId && GrantType == grantType;
        }

        protected internal ClientGrantTypeDto(Guid clientId, [NotNull] string grantType)
        {
            Check.NotNull(grantType, nameof(grantType));

            ClientId = clientId;
            GrantType = grantType;
        }


    }
}