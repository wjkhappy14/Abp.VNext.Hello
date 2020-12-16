using Abp.VNext.Hello.ApiResources;
using Abp.VNext.Hello.Clients;
using Abp.VNext.Hello.Devices;
using Abp.VNext.Hello.Dtos;
using Abp.VNext.Hello.Grants;
using Abp.VNext.Hello.IdentityResources;
using AutoMapper;
using System;
using System.Text;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
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
            CreateMap<ApiResourceSecret, ApiSecretDto>();

            CreateMap<DeviceFlowCodes, DeviceFlowCodesDto>();

            CreateMap<PersistedGrant, PersistedGrantDto>();

            CreateMap<IdentityClaim, IdentityClaimDto>();
            CreateMap<IdentityResource, IdentityResourceDto>();

            CreateMap<ContactDto, Contact>();
            CreateMap<Contact, ContactDto>();

            CreateMap<EztvItemDto, EztvItem>();
            CreateMap<EztvItem, EztvItemDto>()
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => FromBase64String(src.title)))
                .ForMember(dest => dest.size_kb, opt => opt.MapFrom(src => (src.size_bytes / 1024)))
                .ForMember(dest => dest.size_mb, opt => opt.MapFrom(src => (src.size_bytes / 1024 / 1024)))
                .ForMember(dest => dest.size_gb, opt => opt.MapFrom(src => (src.size_bytes / 1024 / 1024 / 1024)))
                .ForMember(dest => dest.torrent_url, opt => opt.MapFrom(src => FromBase64String(src.torrent_url)))
                .ForMember(dest => dest.filename, opt => opt.MapFrom(src => FromBase64String(src.filename)));
        }

        private string FromBase64String(string keyWord)
        {
            byte[] bytes = Convert.FromBase64String(keyWord);
            string str = Encoding.Default.GetString(bytes);
            return str;
        }
    }
}
