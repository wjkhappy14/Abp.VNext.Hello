using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello
{
    public class ContactDto : EntityDto<int>
    {
        public List<AddressDto> Addresses { get; set; }
    }
}
