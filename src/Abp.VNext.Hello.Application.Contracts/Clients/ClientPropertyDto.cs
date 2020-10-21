using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientPropertyDto : EntityDto
    {
        public virtual Guid ClientId { get; set; }

        public virtual string Key { get; set; }

        public virtual string Value { get; set; }

        protected ClientPropertyDto()
        {

        }

        public virtual bool Equals(Guid clientId, [NotNull] string key, string value)
        {
            return ClientId == clientId && Key == key && Value == value;
        }

        protected internal ClientPropertyDto(Guid clientId, [NotNull] string key, [NotNull] string value)
        {
            ClientId = clientId;
            Key = key;
            Value = value;
        }

      
    }
}