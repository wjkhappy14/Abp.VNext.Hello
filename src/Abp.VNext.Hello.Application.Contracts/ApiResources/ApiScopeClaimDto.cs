using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Abp.VNext.Hello.ApiResources
{
    public class ApiScopeClaimDto
    {
        public Guid ApiResourceId { get; protected set; }

        public virtual string Type { get; protected set; }

        [NotNull]
        public string Name { get; protected set; }

        protected ApiScopeClaimDto()
        {

        }

        public virtual bool Equals(Guid apiResourceId, [NotNull] string name, [NotNull] string type)
        {
            return ApiResourceId == apiResourceId && Name == name && Type == type;
        }

        protected internal ApiScopeClaimDto(Guid apiResourceId, [NotNull] string name, [NotNull] string type)
        {
            Check.NotNull(name, nameof(name));

            ApiResourceId = apiResourceId;
            Name = name;
        }

       
    }
}