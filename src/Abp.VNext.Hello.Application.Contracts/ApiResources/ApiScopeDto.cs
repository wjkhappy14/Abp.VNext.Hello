using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.ApiResources
{
    public class ApiScopeDto : EntityDto
    {
        public virtual Guid ApiResourceId { get; protected set; }

        [NotNull]
        public virtual string Name { get; protected set; }

        public virtual string DisplayName { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Required { get; set; }

        public virtual bool Emphasize { get; set; }

        public virtual bool ShowInDiscoveryDocument { get; set; }

        public virtual List<ApiScopeClaimDto> UserClaims { get; protected set; }

        protected ApiScopeDto()
        {

        }

        public virtual bool Equals(Guid apiResourceId, [NotNull] string name)
        {
            return ApiResourceId == apiResourceId && Name == name;
        }

        protected internal ApiScopeDto(
            Guid apiResourceId, 
            [NotNull] string name, 
            string displayName = null, 
            string description = null, 
            bool required = false, 
            bool emphasize = false, 
            bool showInDiscoveryDocument = true)
        {
            Check.NotNull(name, nameof(name));

            ApiResourceId = apiResourceId;
            Name = name;
            DisplayName = displayName ?? name;
            Description = description;
            Required = required;
            Emphasize = emphasize;
            ShowInDiscoveryDocument = showInDiscoveryDocument;

            UserClaims = new List<ApiScopeClaimDto>();
        }

        public virtual void AddUserClaim([NotNull] string type)
        {
            UserClaims.Add(new ApiScopeClaimDto(ApiResourceId, Name, type));
        }

        public virtual void RemoveAllUserClaims()
        {
            UserClaims.Clear();
        }

        public virtual void RemoveClaim(string type)
        {
            UserClaims.RemoveAll(r => r.Type == type);
        }

        public virtual ApiScopeClaimDto FindClaim(string type)
        {
            return UserClaims.FirstOrDefault(r => r.Name == Name && r.Type == type);
        }

       
    }
}