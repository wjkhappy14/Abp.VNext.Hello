using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello
{
    public class PictureDto : EntityDto<int>
    {
        public int ChannelId { get; set; }

        public string FinalTime { get; set; }

        public string CreationTime { get; set; }

        public string QQ { get; set; }

        public string IP { get; set; }

        public string ReferrerUrl { get; set; }

        public decimal OrderMoney { get; set; }
        public string Player { get; set; }

        public int ProductId { get; set; }

        public int MirId { get; set; }

        public string Remark { get; set; }


        public string TransNo { get; set; }

        public int PayStatus { get; set; }
        public int ShipStatus { get; set; }

    }
}
