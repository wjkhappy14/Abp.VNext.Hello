using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientSecretDto : EntityDto<int>
    {

        public virtual string Type { get; protected set; }

        public virtual string Value { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual Guid ClientId { get; protected set; }

        protected ClientSecretDto()
        {

        }

        protected internal ClientSecretDto(
            Guid clientId,
            [NotNull] string value,
            DateTime? expiration,
            string type,
            string description = null
            ) 
        {
            ClientId = clientId;
        }

        public virtual bool Equals(Guid clientId, [NotNull] string value, string type)
        {
            return ClientId == clientId && Value == value && Type == type;
        }

      
    }
}