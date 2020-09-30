using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.IdentityResources
{
    public class IdentityResourceDto : EntityDto<Guid>
    {
        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual bool Required { get; set; }

        public virtual bool Emphasize { get; set; }

        public virtual bool ShowInDiscoveryDocument { get; set; }

        public virtual List<IdentityClaimDto> UserClaims { get; set; }

        public virtual Dictionary<string, string> Properties { get; set; }

        protected IdentityResourceDto()
        {

        }

        public IdentityResourceDto(
            Guid id, 
            [NotNull] string name, 
            string displayName = null, 
            string description = null, 
            bool enabled = true, 
            bool required = false, 
            bool emphasize = false, 
            bool showInDiscoveryDocument = true)
        {
            Check.NotNull(name, nameof(name));

            Id = id;
            Name = name;
            DisplayName = displayName;
            Description = description;
            Enabled = enabled;
            Required = required;
            Emphasize = emphasize;
            ShowInDiscoveryDocument = showInDiscoveryDocument;
            
            UserClaims = new List<IdentityClaimDto>();
            Properties = new Dictionary<string, string>();
        }

        public IdentityResourceDto(Guid id, IdentityServer4.Models.IdentityResource resource)
        {
            Id = id;
            Name = resource.Name;
            DisplayName = resource.DisplayName;
            Description = resource.Description;
            Enabled = resource.Enabled;
            Required = resource.Required;
            Emphasize = resource.Emphasize;
            ShowInDiscoveryDocument = resource.ShowInDiscoveryDocument;
            UserClaims = resource.UserClaims.Select(claimType => new IdentityClaimDto(id, claimType)).ToList();
            Properties = resource.Properties.ToDictionary(x => x.Key, x => x.Value);
        }

        public virtual void AddUserClaim([NotNull] string type)
        {
            UserClaims.Add(new IdentityClaimDto(Id, type));
        }

        public virtual void RemoveAllUserClaims()
        {
            UserClaims.Clear();
        }

        public virtual void RemoveUserClaim(string type)
        {
            UserClaims.RemoveAll(c => c.Type == type);
        }

        public virtual IdentityClaimDto FindUserClaim(string type)
        {
            return UserClaims.FirstOrDefault(c => c.Type == type);
        }
    }
}
