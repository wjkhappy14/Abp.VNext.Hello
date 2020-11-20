using Abp.VNext.Hello.ApiResources;
using Abp.VNext.Hello.Clients;
using Abp.VNext.Hello.Devices;
using Abp.VNext.Hello.Dtos;
using Abp.VNext.Hello.Grants;
using Abp.VNext.Hello.IdentityResources;
using AutoMapper;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.Grants;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Abp.VNext.Hello
{
    public class HelloApplicationAutoMapperProfile : Profile
    {
        public HelloApplicationAutoMapperProfile()
        {
            CreateMap<Country, CountryDto>().ForMember(dest => dest.NationalFlagUrl, opt => opt.MapFrom(src => src.NationalFlag));
            CreateMap<CountryDto, Country>().ForMember(dest => dest.NationalFlag, opt => opt.MapFrom(src => src.NationalFlagUrl));
            CreateMap<StateProvince, StateProvinceDto>();
            CreateMap<StateProvinceDto, StateProvince>();
            CreateMap<City, CityDto>();//.Ignore(x=>x.);
            CreateMap<CityDto, City>();

            CreateMap<Client, ClientDto>();
            CreateMap<ClientClaim, ClientClaimDto>();
            CreateMap<ClientCorsOrigin, ClientCorsOriginDto>();
            CreateMap<ClientGrantType, ClientGrantTypeDto>();
            CreateMap<ClientIdPRestriction, ClientIdPRestrictionDto>();
            CreateMap<ClientPostLogoutRedirectUri, ClientPostLogoutRedirectUriDto>();
            CreateMap<ClientProperty, ClientPropertyDto>();
            CreateMap<ClientRedirectUri, ClientRedirectUriDto>();
            CreateMap<ClientScope, ClientScopeDto>();
            CreateMap<ClientSecret, ClientSecretDto>();

            CreateMap<ApiResource, ApiResourceDto>();
            CreateMap<ApiScopeClaim, ApiScopeClaimDto>();
            CreateMap<ApiResourceClaim, ApiResourceClaimDto>();
            CreateMap<ApiScope, ApiScopeDto>();
            CreateMap<ApiSecret, ApiSecretDto>();

            CreateMap<DeviceFlowCodes, DeviceFlowCodesDto>();

            CreateMap<PersistedGrant, PersistedGrantDto>();

            CreateMap<IdentityClaim, IdentityClaimDto>();
            CreateMap<IdentityResource, IdentityResourceDto>();

        }
    }
}
