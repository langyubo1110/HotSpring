using Autofac;
using Autofac.Integration.Mvc;
using HotSpringProject.App_Start;
using HotSpringProject.Entity;
using HotSpringProject.Job;
using OA_AutoWork.App_Start;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Extras.Quartz;

namespace HotSpringProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //容器注册
            AutofacRegister();
            //GlobalFilters.Filters.Add(new AuthorizationFilter());//拦截器
            AutoMapperConfig.Config();


            //BackUpDataBase.Initialize();
            //备份数据库
            
            //SalaryPost.Initialize();//定时调度薪资发放

            // 创建 Quartz 调度器
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler().Result;
            //设置 Quartz 作业工厂，以便解析作业实例中的依赖项
            scheduler.JobFactory = new Job.AutofacJobFactory(AutofacDependencyResolver.Current.RequestLifetimeScope);
            // 开启调度器
            scheduler.Start().Wait();

            // 创建 JobDetail
            IJobDetail writeDatajobDetail = JobBuilder.Create<MyJob>()
                                            .WithIdentity("myJob")
                                            .Build();
            // 创建触发器
            ITrigger writeDatatrigger = TriggerBuilder.Create()
                                             .WithIdentity("myTrigger")
                                             .StartNow()
                                             .WithCronSchedule("0 0 8 * * ?")
                                             //.WithSimpleSchedule(x => x
                                             //.WithIntervalInSeconds(60) // 每 5 秒执行一次
                                             //.RepeatForever())
                                             .Build();

            // 将 JobDetail 和 Trigger 绑定到调度器
            scheduler.ScheduleJob(writeDatajobDetail, writeDatatrigger).Wait();
        }



        public static void AutofacRegister()
        {
            //容器注册
            var builder = new ContainerBuilder();
            //1.单个依赖注入  
            builder.RegisterType<HotSpringDbContext>();
            //2.依赖注入当前应用程序下的所有Controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//指定注入方式为属性注入
            //3.依赖注入按程序集注入
            Assembly asmService = Assembly.Load("HotSpringProjectService");
            builder.RegisterAssemblyTypes(asmService).Where(t => !t.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();
            Assembly asmRepository = Assembly.Load("HotSpringProjectRepository");
            builder.RegisterAssemblyTypes(asmRepository).Where(t => !t.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            ////注入任务类
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            //把任务类注入到autofac
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(MyJob).Assembly));

            ////注入任务类
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            //把任务类注入到autofac
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(DataBaseJob).Assembly));

            //容器构建
            var container = builder.Build();
            //解析器替换
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
