using System;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{
    public class CountryDto : EntityDto<int>
    {
        public string Account { get; set; }
        

        public string Address { get; set; }


        public string BankName { get; set; }


        public string BankCode { get; set; }


        public string Remark { get; set; }


        public string BankCity { get; set; }

        public decimal Fee { get; set; }



        public int MerchantId
        {
            get;
            set;
        }

        public string PayeeName
        {
            get;
            set;
        }

        public decimal PayMoney { get; set; }


        public DateTime? PayTime { get; set; }


        public DateTime CreationTime { get; set; }

        public decimal ReqMoney
        {
            get;
            set;
        }

        public DateTime ReqTime
        {
            get;
            set;
        }

        public string SerialNo
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public string DaiFuTradeNo
        {
            get;
            set;
        }

        public decimal SettleCost
        {
            get;
            set;
        }

        public string StatusDes()
        {
            return "";//((EncashStatus)Status).ToDescription();
        }
    }

}
