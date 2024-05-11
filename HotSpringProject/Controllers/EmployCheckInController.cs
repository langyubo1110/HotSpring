using DotNet.Utilities;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class EmployCheckInController : Controller
    {
        private readonly IEmployCheckInService _db;

        public EmployCheckInController(IEmployCheckInService employCheckInService)
        {
            _db = employCheckInService;
        }
        /*员工签到表的控制器
         * 刘星宇
         * 2024-05-08
         */
        // GET: EmoloyCheckIn
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Insert()
        {
            int type = Convert.ToInt32(Request.QueryString["selectedValue"]);
            int empId = 1;
            ResMessage res = _db.Add(empId, type);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}