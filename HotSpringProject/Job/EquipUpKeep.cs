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
                                             //.StartNow()
                                             .WithDailyTimeIntervalSchedule(x => x
                                             .OnEveryDay()
                                             .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(15, 40))
                                             .WithIntervalInHours(24))
                                             //.StartAt(DateTime.UtcNow) // 设置触发器的开始时间为当前时间
                                             //.WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(30, 0, 0))
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