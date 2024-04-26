using Autofac;
using Autofac.Integration.Mvc;
using HotSpringProject.Entity;
using OA_AutoWork.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
            //AutoMapper注册
            AutoMapperConfig.Config();
            
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
            //容器构建
            var container = builder.Build();
            //解析器替换
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
