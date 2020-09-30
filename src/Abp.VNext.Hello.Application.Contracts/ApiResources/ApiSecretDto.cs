using System;
using JetBrains.Annotations;

namespace Abp.VNext.Hello.ApiResources
{
    public class ApiSecretDto
    {
        public virtual Guid ApiResourceId { get; protected set; }


        public virtual string Type { get; protected set; }

        public virtual string Value { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime? Expiration { get; set; }

        protected ApiSecretDto()
        {

        }

        public virtual bool Equals(Guid apiResourceId, [NotNull] string value, string type )
        {
            return ApiResourceId == apiResourceId && Value == value && Type == type;
        }

        protected internal ApiSecretDto(
            Guid apiResourceId,
            [NotNull] string value, 
            DateTime? expiration, 
            string type , 
            string description = null
            ) 
        {
            ApiResourceId = apiResourceId;
        }

       
    }
}