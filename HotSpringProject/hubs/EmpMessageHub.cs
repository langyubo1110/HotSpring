using HotSpringProject.Entity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotSpringProject.hubs
{

    [HubName("EmpMessageService")]
    public class EmpMessageHub:Hub
    {
        public static void Show(int number)
        {
            IHubContext content = GlobalHost.ConnectionManager.GetHubContext<EmpMessageHub>();
            content.Clients.All.getNumber(number);
        }
        //推全表数据
        public static void List(List<EmployMessageVO> list)
        {
            IHubContext content = GlobalHost.ConnectionManager.GetHubContext<EmpMessageHub>();
            content.Clients.All.getList(JsonConvert.SerializeObject(list));
        }
    }
}