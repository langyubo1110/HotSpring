using Autofac.Integration.Mvc;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotSpringProject.Job
{
    public class EquipUpKeep
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
            IJobDetail writeDatajobDetail = JobBuilder.Create<MyJob>()
                                            .WithIdentity("myJob")
                                            .Build();
            // 创建触发器
            ITrigger writeDatatrigger = TriggerBuilder.Create()
                                             .WithIdentity("myTrigger")
                                            

                                 .WithDailyTimeIntervalSchedule(x => x
                                 .OnEveryDay()
                                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9,0))
                                 .WithIntervalInHours(24))
                                 .Build();

            ScheduleJob(writeDatajobDetail, writeDatatrigger);
        }

        private static void ScheduleJob(IJobDetail writeDatajobDetail, ITrigger writeDatatrigger)
        {
            if (_scheduler == null)
            {
                throw new InvalidOperationException("Scheduler has not been started. Call Initialize method first.");
            }
            _scheduler.ScheduleJob(writeDatajobDetail, writeDatatrigger).Wait();
        }
    }
}