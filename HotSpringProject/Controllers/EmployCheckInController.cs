using DotNet.Utilities;
using HotSpringProject.Entity;
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
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int empId = employEmp.id;
            ResMessage res = _db.Verify(empId);
            if (res.code==200)
            {
                return Json(ResMessage.Success("请勿重复签到"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResMessage res1 = _db.Add(empId, type);
                ViewBag.IsSignedIn = (res1.code == 200);
                return Json(res1, JsonRequestBehavior.AllowGet);

            }

        }
    }
}