using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{
    public class CountryDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string FormalName { get; set; }
        public string IsoAlpha3Code { get; set; }
        public string IsoNumericCode { get; set; }
        public string CountryType { get; set; }
        public string Continent { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public string Border { get; set; }
        public string ChineseName { get; set; }
        public string IsoAlpha2Code { get; set; }
        public long Population { get; set; }
        public string NationalFlagUrl { get; set; }
    }
}
