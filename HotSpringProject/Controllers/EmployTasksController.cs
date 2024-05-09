using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class EmployTasksController : Controller
    {
        /*员工个人表的控制器
        * 刘星宇
        * 2024-05-08
        */
        // GET: EmployTasks
        public ActionResult RepairTasks()
        {
            return View();
        }
        public ActionResult UpKeepTasks() 
        {
            return View();
        }
    }
}