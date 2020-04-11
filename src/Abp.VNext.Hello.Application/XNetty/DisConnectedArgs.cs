using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Abp.VNext.Hello.XNetty
{
    public class DisConnectedArgs
    {
        public EndPoint EndPoint { get; set; }

        public string ContextId { get; set; }

        public override string ToString() => $"ContextId:{ContextId}\tEndPoint:{EndPoint}";
    }

}
