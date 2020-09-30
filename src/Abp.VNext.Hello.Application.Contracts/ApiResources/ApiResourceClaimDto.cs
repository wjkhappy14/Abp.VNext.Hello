using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.ApiResources
{
    public class ApiResourceClaimDto : EntityDto<Guid>
    {
        public  Guid ApiResourceId { get; set; }


        public  string Type { get; protected set; }

        protected ApiResourceClaimDto()
        {

        }

        public virtual bool Equals(Guid apiResourceId, [NotNull] string type)
        {
            return ApiResourceId == apiResourceId && Type == type;
        }

        protected internal ApiResourceClaimDto(Guid apiResourceId, [NotNull] string type)
        {
            ApiResourceId = apiResourceId;
        }

       
    }
}