using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientPostLogoutRedirectUriDto : EntityDto
    {
        public virtual Guid ClientId { get; protected set; }

        public virtual string PostLogoutRedirectUri { get; protected set; }
        
        protected ClientPostLogoutRedirectUriDto()
        {

        }

        public virtual bool Equals(Guid clientId, [NotNull] string uri)
        {
            return ClientId == clientId && PostLogoutRedirectUri == uri;
        }

        protected internal ClientPostLogoutRedirectUriDto(Guid clientId, [NotNull] string postLogoutRedirectUri)
        {
            Check.NotNull(postLogoutRedirectUri, nameof(postLogoutRedirectUri));

            ClientId = clientId;
            PostLogoutRedirectUri = postLogoutRedirectUri;
        }
       
    }
}