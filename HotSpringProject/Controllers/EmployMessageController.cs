using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class EmployMessageController : Controller
    {
        // GET: Chat 站内信
        public ActionResult MesIndex()
        {
            return View();
        }
        public ActionResult MesWrite()
        {
            return View();
        }
       
    }
}