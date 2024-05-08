using Autofac;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotSpringProject.Job
{
    public class AutofacJobFactory : IJobFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacJobFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                var jobDetail = bundle.JobDetail;
                var jobType = jobDetail.JobType;
                return (IJob)_lifetimeScope.Resolve(jobType);
            }
            catch (Exception ex)
            {
                throw new SchedulerException($"Problem while instantiating job '{bundle.JobDetail.Key}'", ex);
            }
        }

        public void ReturnJob(IJob job)
        {
            // Optionally, you can release resolved job instances back to the container here.
            (job as IDisposable)?.Dispose();
        }
    }
}