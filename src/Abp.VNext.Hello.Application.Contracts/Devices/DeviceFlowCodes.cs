using System;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Devices
{
    public class DeviceFlowCodesDto : EntityDto<Guid>
    {
        public virtual string DeviceCode { get; set; }

        public virtual string UserCode { get; set; }

        public virtual string SubjectId { get; set; }

        public virtual string ClientId { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual string Data { get; set; }

        protected DeviceFlowCodesDto()
        {

        }

        public DeviceFlowCodesDto(Guid id)
        {

        }
    }
}