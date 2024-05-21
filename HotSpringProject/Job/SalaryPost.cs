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
            IJobDetail salaryjobDetail = JobBuilder.Create<SalaryPostJob>()
                                             .WithIdentity("SalaryPostJob")
                                             .Build();

            // 创建触发器
            ITrigger salarytrigger = TriggerBuilder.Create()
                                             .WithIdentity("salaryTrigger")
                                             //.WithCronSchedule("0 0 0 1 * ? *")
                                             ////秒 分 时 日 月 周 年
                                             ////每月1号0点执行
                                             //.Build();

                .WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(1, 0, 0)) // 设置为每月1日的0点0分触发
                .Build();


            ScheduleJob(salaryjobDetail, salarytrigger);
        }

        private static void ScheduleJob(IJobDetail salaryjobDetail, ITrigger salarytrigger)
        {
            if (_scheduler == null)
            {
                throw new InvalidOperationException("Scheduler has not been started. Call Initialize method first.");
            }
            _scheduler.ScheduleJob(salaryjobDetail, salarytrigger).Wait();
        }
    }
}