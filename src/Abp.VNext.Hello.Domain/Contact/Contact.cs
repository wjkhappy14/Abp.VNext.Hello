using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Abp.VNext.Hello
{
    public class Contact : Entity<int>
    {
        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string QQ { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public string Languages { get; set; }
        public string UserPreferences { get; set; }
        public List<Address> Address { get; set; }
    }
}
