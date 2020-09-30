using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientScopeDto : EntityDto
    {
        public virtual Guid ClientId { get; protected set; }

        public virtual string Scope { get; protected set; }

        protected ClientScopeDto()
        {

        }

        public virtual bool Equals(Guid clientId, [NotNull] string scope)
        {
            return ClientId == clientId && Scope == scope;
        }

        protected internal ClientScopeDto(Guid clientId, string scope)
        {
            ClientId = clientId;
            Scope = scope;
        }
    }
}