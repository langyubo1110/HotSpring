using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(HotSpringProject.App_Start.Startup))]
namespace HotSpringProject.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //注册管道，使用默认虚拟地址，根目录下的signalr
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}