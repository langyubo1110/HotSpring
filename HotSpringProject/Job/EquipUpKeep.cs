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
                                             //.WithCronSchedule("0 0 8 * * ?")
                                             //.WithSimpleSchedule(x => x.WithIntervalInHours(24).RepeatForever()) // 设置触发频率为每24小时
                                             //.Build();

                                 .WithDailyTimeIntervalSchedule(x => x
                                 .OnEveryDay()
                                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8,0))
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