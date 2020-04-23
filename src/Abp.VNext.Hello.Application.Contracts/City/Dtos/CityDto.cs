using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{

    public class CityDto : EntityDto<int>
    {
        public virtual string Name { get; set; }
        public virtual string ChineseName { get; set; }
        public virtual int StateProvinceId { get; set; }

    }
}
