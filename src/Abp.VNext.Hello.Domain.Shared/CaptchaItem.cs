using System;

namespace Abp.VNext.Hello
{
    public class CaptchaItem
    {
        public string Code { get; set; }

        public Guid SessionId { get; set; }

    }
}
