using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientClaimDto : EntityDto
    {
        public virtual Guid ClientId { get; set; }

        public virtual string Type { get; set; }

        public virtual string Value { get; set; }

        protected ClientClaimDto()
        {

        }

        public virtual bool Equals(Guid clientId, string value, string type)
        {
            return ClientId == clientId && Type == type && Value == value;
        }

        protected internal ClientClaimDto(Guid clientId, [NotNull] string type, string value)
        {
            ClientId = clientId;
            Type = type;
            Value = value;
        }

       
    }
}