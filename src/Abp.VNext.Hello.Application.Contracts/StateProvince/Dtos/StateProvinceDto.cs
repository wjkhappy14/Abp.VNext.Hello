using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{
    public  class StateProvinceDto : EntityDto<int>
    {
        public int MerchantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public string ProductNo { get; set; }
        public int ChannelId { get; set; }

        public decimal ProductRate { get; set; }

        public decimal ChannelRate { get; set; }

        public decimal AgentRate { get; set; }

    }
}
