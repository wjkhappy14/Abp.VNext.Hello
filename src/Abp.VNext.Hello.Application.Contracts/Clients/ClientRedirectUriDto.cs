using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientRedirectUriDto : EntityDto
    {
        public virtual Guid ClientId { get; protected set; }

        public virtual string RedirectUri { get; protected set; }

        protected ClientRedirectUriDto()
        {

        }

        public virtual bool Equals(Guid clientId, [NotNull] string uri)
        {
            return ClientId == clientId && RedirectUri == uri;
        }

        protected internal ClientRedirectUriDto(Guid clientId, [NotNull] string redirectUri)
        {
            ClientId = clientId;
            RedirectUri = redirectUri;
        }
    }
}