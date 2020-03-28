using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{
   public class CityDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public bool Status { get; set; }
    }
}
