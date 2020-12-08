﻿using System;
using Volo.Abp.Domain.Entities;

namespace Abp.VNext.Hello
{
    public  class Address : Entity<int>
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public int StateProvinceId { get; set; }
        public string Details { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int AddressType { get; set; }
        public DateTime CreationTime { get; set; }
        public string ExtraProperties { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool IsDeleted { get; set; }
    }
}
