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
using HotSpringProject.Filter;
using System.Data.SqlClient;
using System.Configuration;

namespace HotSpringProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["HS"].ConnectionString);

            //容器注册
            AutofacRegister();
            GlobalFilters.Filters.Add(new AuthorizationFilter());//拦截器
            AutoMapperConfig.Config();

            BackUpDataBase.Initialize();
            //备份数据库

            //定时调度薪资发放
            SalaryPost.Initialize();
            
            // 设备保养生成调度器
            EquipUpKeep.Initialize();

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

            //注入任务类
            builder.RegisterModule(new QuartzAutofacFactoryModule());

            //把任务类注入到autofac
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(MyJob).Assembly));

            //把任务类注入到autofac
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(DataBaseJob).Assembly));
            
            //把任务类注入到autofac
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(SalaryPostJob).Assembly));

            //容器构建
            var container = builder.Build();

            //解析器替换
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        protected void Application_End(object sender, EventArgs e)
        {
            //当被监测的数据库中的数据发生变化时,SqlDependency会自动触发OnChange事件来通知应用程序,从而达到让系统自动更新数据(或缓存)的目的。
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["HS"].ConnectionString);
        }
    }
}
