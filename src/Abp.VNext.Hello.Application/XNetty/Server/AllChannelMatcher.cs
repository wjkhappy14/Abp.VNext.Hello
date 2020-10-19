using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;

namespace Abp.VNext.Hello.XNetty.Server
{

    public class AllChannelMatcher : IChannelMatcher
    {
        readonly IChannelId id;

        public AllChannelMatcher(IChannelId id)
        {
            this.id = id;
        }

        public bool Matches(IChannel channel) => channel.Id == this.id;// channel.Id != this.id;
    }

}
