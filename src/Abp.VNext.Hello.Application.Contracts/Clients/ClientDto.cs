using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Clients
{
    public class ClientDto : EntityDto<Guid>
    {
        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string Description { get; set; }

        public string ClientUri { get; set; }

        public string LogoUri { get; set; }

        public bool Enabled { get; set; } = true;

        public string ProtocolType { get; set; }

        public bool RequireClientSecret { get; set; }

        public bool RequireConsent { get; set; }

        public bool AllowRememberConsent { get; set; }

        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        public bool RequirePkce { get; set; }

        public bool AllowPlainTextPkce { get; set; }

        public bool AllowAccessTokensViaBrowser { get; set; }

        public string FrontChannelLogoutUri { get; set; }

        public bool FrontChannelLogoutSessionRequired { get; set; }

        public string BackChannelLogoutUri { get; set; }

        public bool BackChannelLogoutSessionRequired { get; set; }

        public bool AllowOfflineAccess { get; set; }

        public int IdentityTokenLifetime { get; set; }

        public int AccessTokenLifetime { get; set; }

        public int AuthorizationCodeLifetime { get; set; }

        public int? ConsentLifetime { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; }

        public int SlidingRefreshTokenLifetime { get; set; }

        public int RefreshTokenUsage { get; set; }

        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        public int RefreshTokenExpiration { get; set; }

        public int AccessTokenType { get; set; }

        public bool EnableLocalLogin { get; set; }

        public bool IncludeJwtId { get; set; }

        public bool AlwaysSendClientClaims { get; set; }

        public string ClientClaimsPrefix { get; set; }

        public string PairWiseSubjectSalt { get; set; }

        public int? UserSsoLifetime { get; set; }

        public string UserCodeType { get; set; }

        public int DeviceCodeLifetime { get; set; } = 300;

        public List<ClientScopeDto> AllowedScopes { get; set; }

        public List<ClientSecretDto> ClientSecrets { get; set; }

        public List<ClientGrantTypeDto> AllowedGrantTypes { get; set; }

        public List<ClientCorsOriginDto> AllowedCorsOrigins { get; set; }

        public List<ClientRedirectUriDto> RedirectUris { get; set; }

        public List<ClientPostLogoutRedirectUriDto> PostLogoutRedirectUris { get; set; }

        public List<ClientIdPRestrictionDto> IdentityProviderRestrictions { get; set; }

        public List<ClientClaimDto> Claims { get; set; }

        public List<ClientPropertyDto> Properties { get; set; }


        public ClientDto()
        {
            // ProtocolType="";// IdentityServerConstants.ProtocolTypes.OpenIdConnect
            // RequireClientSecret = true;
            // RequireConsent = true;
            // AllowRememberConsent = true;
            // FrontChannelLogoutSessionRequired = true;
            // BackChannelLogoutSessionRequired = true;
            // IdentityTokenLifetime = 300;
            // AccessTokenLifetime = 3600;
            // AuthorizationCodeLifetime = 300;
            // AbsoluteRefreshTokenLifetime = 2592000;
            // SlidingRefreshTokenLifetime = 1296000;
            // RefreshTokenUsage = (int)TokenUsage.OneTimeOnly;
            // RefreshTokenExpiration = (int)TokenExpiration.Absolute;
            // AccessTokenType = (int)IdentityServer4.Models.AccessTokenType.Jwt;
            // EnableLocalLogin = true;
            // ClientClaimsPrefix = "client_";

            AllowedScopes = new List<ClientScopeDto>();
            ClientSecrets = new List<ClientSecretDto>();
            AllowedGrantTypes = new List<ClientGrantTypeDto>();
            AllowedCorsOrigins = new List<ClientCorsOriginDto>();
            RedirectUris = new List<ClientRedirectUriDto>();
            PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUriDto>();
            IdentityProviderRestrictions = new List<ClientIdPRestrictionDto>();
            Claims = new List<ClientClaimDto>();
            Properties = new List<ClientPropertyDto>();
        }

        public void AddGrantType([NotNull] string grantType)
        {
            AllowedGrantTypes.Add(new ClientGrantTypeDto(Id, grantType));
        }

        public void RemoveAllAllowedGrantTypes()
        {
            AllowedGrantTypes.Clear();
        }

        public void RemoveGrantType(string grantType)
        {
            AllowedGrantTypes.RemoveAll(r => r.GrantType == grantType);
        }

        public ClientGrantTypeDto FindGrantType(string grantType)
        {
            return AllowedGrantTypes.FirstOrDefault(r => r.GrantType == grantType);
        }

        public void AddSecret([NotNull] string value, DateTime? expiration, string type, string description)
        {
            ClientSecrets.Add(new ClientSecretDto(Id, value, expiration, type, description));
        }

        public void RemoveSecret([NotNull] string value, string type)
        {
            ClientSecrets.RemoveAll(s => s.Value == value && s.Type == type);
        }

        public ClientSecretDto FindSecret([NotNull] string value, string type)
        {
            return ClientSecrets.FirstOrDefault(s => s.Type == type && s.Value == value);
        }

        public void AddScope([NotNull] string scope)
        {
            AllowedScopes.Add(new ClientScopeDto(Id, scope));
        }

        public void RemoveAllScopes()
        {
            AllowedScopes.Clear();
        }

        public void RemoveScope(string scope)
        {
            AllowedScopes.RemoveAll(r => r.Scope == scope);
        }

        public ClientScopeDto FindScope(string scope)
        {
            return AllowedScopes.FirstOrDefault(r => r.Scope == scope);
        }

        public void AddCorsOrigin([NotNull] string origin)
        {
            AllowedCorsOrigins.Add(new ClientCorsOriginDto(Id, origin));
        }

        public void AddRedirectUri([NotNull] string redirectUri)
        {
            RedirectUris.Add(new ClientRedirectUriDto(Id, redirectUri));
        }

        public void AddPostLogoutRedirectUri([NotNull] string postLogoutRedirectUri)
        {
            PostLogoutRedirectUris.Add(new ClientPostLogoutRedirectUriDto(Id, postLogoutRedirectUri));
        }

        public void RemoveAllCorsOrigins()
        {
            AllowedCorsOrigins.Clear();
        }

        public void RemoveCorsOrigin(string uri)
        {
            AllowedCorsOrigins.RemoveAll(c => c.Origin == uri);
        }

        public void RemoveAllRedirectUris()
        {
            RedirectUris.Clear();
        }

        public void RemoveRedirectUri(string uri)
        {
            RedirectUris.RemoveAll(r => r.RedirectUri == uri);
        }

        public void RemoveAllPostLogoutRedirectUris()
        {
            PostLogoutRedirectUris.Clear();
        }

        public void RemovePostLogoutRedirectUri(string uri)
        {
            PostLogoutRedirectUris.RemoveAll(p => p.PostLogoutRedirectUri == uri);
        }

        public ClientCorsOriginDto FindCorsOrigin(string uri)
        {
            return AllowedCorsOrigins.FirstOrDefault(c => c.Origin == uri);
        }

        public ClientRedirectUriDto FindRedirectUri(string uri)
        {
            return RedirectUris.FirstOrDefault(r => r.RedirectUri == uri);
        }

        public ClientPostLogoutRedirectUriDto FindPostLogoutRedirectUri(string uri)
        {
            return PostLogoutRedirectUris.FirstOrDefault(p => p.PostLogoutRedirectUri == uri);
        }

        public void AddProperty([NotNull] string key, [NotNull] string value)
        {
            Properties.Add(new ClientPropertyDto(Id, key, value));
        }

        public void RemoveAllProperties()
        {
            Properties.Clear();
        }

        public void RemoveProperty(string key, string value)
        {
            Properties.RemoveAll(c => c.Value == value && c.Key == key);
        }

        public ClientPropertyDto FindProperty(string key, string value)
        {
            return Properties.FirstOrDefault(c => c.Key == key && c.Value == value);
        }

        public void AddClaim([NotNull] string value, string type)
        {
            Claims.Add(new ClientClaimDto(Id, type, value));
        }

        public void RemoveAllClaims()
        {
            Claims.Clear();
        }

        public void RemoveClaim(string value, string type)
        {
            Claims.RemoveAll(c => c.Value == value && c.Type == type);
        }

        public ClientClaimDto FindClaim(string value, string type)
        {
            return Claims.FirstOrDefault(c => c.Type == type && c.Value == value);
        }

        public void AddIdentityProviderRestriction([NotNull] string provider)
        {
            IdentityProviderRestrictions.Add(new ClientIdPRestrictionDto(Id, provider));
        }

        public void RemoveAllIdentityProviderRestrictions()
        {
            IdentityProviderRestrictions.Clear();
        }

        public void RemoveIdentityProviderRestriction(string provider)
        {
            IdentityProviderRestrictions.RemoveAll(r => r.Provider == provider);
        }

        public ClientIdPRestrictionDto FindIdentityProviderRestriction(string provider)
        {
            return IdentityProviderRestrictions.FirstOrDefault(r => r.Provider == provider);
        }
    }
}