using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.IdentityResources
{
    public class IdentityClaimDto :  EntityDto<Guid>
    {
        public virtual Guid IdentityResourceId { get; set; }

        protected IdentityClaimDto()
        {

        }
        public string Type { get; protected set; }
        public virtual bool Equals(Guid identityResourceId, [NotNull] string type)
        {
            return IdentityResourceId == identityResourceId && Type == type;
        }

        protected internal IdentityClaimDto(Guid identityResourceId, [NotNull] string type)
        {
            IdentityResourceId = identityResourceId;
        }

       
    }
}