using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Abp.VNext.Hello.Jobs
{
    public class EmailSenderJob : BackgroundJob<EmailSenderJobArgs>, ITransientDependency
    {
        public EmailSenderJob()
        {

        }

        public override void Execute(EmailSenderJobArgs args)
        {
            Logger.LogDebug("EmailSenderJob");
        }
    }
}
