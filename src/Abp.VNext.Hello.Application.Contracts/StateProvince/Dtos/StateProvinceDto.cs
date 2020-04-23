using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{

    public class StateProvinceDto : EntityDto<int>
    {
        public virtual string StateProvinceCode { get; set; }
        public virtual string StateProvinceName { get; set; }

        public virtual string ChineseName { get; set; }
        public virtual int CountryId { get; set; }

    }
}
