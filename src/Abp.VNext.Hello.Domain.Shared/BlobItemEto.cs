using Volo.Abp.EventBus;

namespace Abp.VNext.Hello
{
    [EventName("Abp.VNext.Hello.Blob")]
    public class BlobItemEto
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }
    }
}
