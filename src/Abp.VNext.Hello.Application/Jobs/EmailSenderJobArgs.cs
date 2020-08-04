using Volo.Abp.BackgroundJobs;

namespace Abp.VNext.Hello.Jobs
{
    [BackgroundJobName("EmailSenderJob")]

    public class EmailSenderJobArgs
    {
    }
}
