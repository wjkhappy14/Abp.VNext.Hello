using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.ApiResources
{
    public class ApiResourceDto : EntityDto<Guid>
    {
        [NotNull]
        public virtual string Name { get; protected set; }

        public virtual string DisplayName { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual List<ApiSecretDto> Secrets { get; protected set; }

        public virtual List<ApiScopeDto> Scopes { get; protected set; }

        public virtual List<ApiResourceClaimDto> UserClaims { get; protected set; }

        public virtual Dictionary<string, string> Properties { get; protected set; }

        protected ApiResourceDto()
        {

        }

        public ApiResourceDto(Guid id, [NotNull] string name, string displayName = null, string description = null)
        {

            Id = id;

            Name = name;

            DisplayName = displayName;
            Description = description;

            Enabled = true;

            Secrets = new List<ApiSecretDto>();
            Scopes = new List<ApiScopeDto>();
            UserClaims = new List<ApiResourceClaimDto>();
            Properties = new Dictionary<string, string>();

            Scopes.Add(new ApiScopeDto(id, name, displayName, description));
        }

        public virtual void AddSecret(
            [NotNull] string value, 
            DateTime? expiration,
            string type,
            string description)
        {
            Secrets.Add(new ApiSecretDto(Id, value, expiration, type, description));
        }

        public virtual void RemoveSecret([NotNull] string value, string type )
        {
            Secrets.RemoveAll(s => s.Value == value && s.Type == type);
        }

        public virtual ApiSecretDto FindSecret([NotNull] string value, string type )
        {
            return Secrets.FirstOrDefault(s => s.Type == type && s.Value == value);
        }

        public virtual ApiScopeDto AddScope(
            [NotNull] string name,
            string displayName = null,
            string description = null,
            bool required = false,
            bool emphasize = false,
            bool showInDiscoveryDocument = true)
        {
            var scope = new ApiScopeDto(Id, name, displayName, description, required, emphasize, showInDiscoveryDocument);
            Scopes.Add(scope);
            return scope;
        }

        public virtual void AddUserClaim([NotNull] string type)
        {
            UserClaims.Add(new ApiResourceClaimDto(Id, type));
        }

        public virtual void RemoveAllUserClaims()
        {
            UserClaims.Clear();
        }

        public virtual void RemoveClaim(string type)
        {
            UserClaims.RemoveAll(c => c.Type == type);
        }

        public virtual ApiResourceClaimDto FindClaim(string type)
        {
            return UserClaims.FirstOrDefault(c => c.Type == type);
        }

        public virtual void RemoveAllSecrets()
        {
            Secrets.Clear();
        }

        public virtual void RemoveAllScopes()
        {
            foreach (var scope in Scopes)
            {
                scope.RemoveAllUserClaims();
            }
            Scopes.Clear();
        }

        public virtual void RemoveScope(string name)
        {
            Scopes.RemoveAll(r => r.Name == name);
        }

        public virtual ApiScopeDto FindScope(string name)
        {
            return Scopes.FirstOrDefault(r => r.Name == name);
        }
    }
}
