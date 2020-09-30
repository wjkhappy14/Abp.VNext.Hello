using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientCorsOriginDto : EntityDto
    {
        public virtual Guid ClientId { get; protected set; }

        public virtual string Origin { get; protected set; }

        protected ClientCorsOriginDto()
        {

        }

        public virtual bool Equals(Guid clientId, [NotNull] string uri)
        {
            return ClientId == clientId && Origin == uri;
        }

        protected internal ClientCorsOriginDto(Guid clientId, [NotNull] string origin)
        {

            ClientId = clientId;
            Origin = origin;
        }
    }
}