using Autofac.Integration.Mvc;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotSpringProject.Job
{
    public class SalaryPost
    {
        private static IScheduler _scheduler;

        public static void Initialize()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = new Job.AutofacJobFactory(AutofacDependencyResolver.Current.RequestLifetimeScope);
            _scheduler.Start().Wait();

            // 配置和调度任务
            ConfigureScheduledJobs();
        }

        private static void ConfigureScheduledJobs()
        {
            // 创建 JobDetail
            IJobDetail DataBasejobDetail = JobBuilder.Create<SalaryPostJob>()
                                             .WithIdentity("SalaryPostJob")
                                             .Build();

            // 创建触发器
            ITrigger DataBasetrigger = TriggerBuilder.Create()
                                             .WithIdentity("myTrigger")
                                             .StartNow()
                                             //.StartAt(DateTime.UtcNow) // 设置触发器的开始时间为当前时间
                                             //.WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(30, 0, 0))
                                             .Build();

            ScheduleJob(DataBasejobDetail, DataBasetrigger);
        }

        private static void ScheduleJob(IJobDetail DataBasejobDetail, ITrigger DataBasetrigger)
        {
            if (_scheduler == null)
            {
                throw new InvalidOperationException("Scheduler has not been started. Call Initialize method first.");
            }
            _scheduler.ScheduleJob(DataBasejobDetail, DataBasetrigger).Wait();
        }
    }
}