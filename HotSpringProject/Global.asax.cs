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
            //����ע��
            AutofacRegister();
            //AutoMapperע��
            AutoMapperConfig.Config();
        }
        public static void AutofacRegister()
        {
            //����ע��
            var builder = new ContainerBuilder();
            //1.��������ע��
            builder.RegisterType<HotSpringDbContext>();
            //2.����ע�뵱ǰӦ�ó����µ�����Controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//ָ��ע�뷽ʽΪ����ע��
            //3.����ע�밴����ע��
            Assembly asmService = Assembly.Load("HotSpringProjectService");
            builder.RegisterAssemblyTypes(asmService).Where(t => !t.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();
            Assembly asmRepository = Assembly.Load("HotSpringProjectRepository");
            builder.RegisterAssemblyTypes(asmRepository).Where(t => !t.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();
            //��������
            var container = builder.Build();
            //�������滻
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
