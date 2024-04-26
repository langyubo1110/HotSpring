using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;

namespace HotSpringProject.Controllers
{
    public class EmployEmpController : Controller
    {
        /*员工表的控制器
         * 刘星宇
         * 2024-04-25
         */
        // GET: EmployEmp
        private readonly IEmployEmpService _dbService;

        public EmployEmpController(IEmployEmpService employEmpService)
        {
            _dbService = employEmpService;
        }
        public ActionResult EmployManager()
        {
            return View();
        }
        public ActionResult Detail(int id=0)
        {
            ViewBag.id = id;
            return View();
        }
        public JsonResult Employ(int page, int limit) 
        {
            ResMessage res = _dbService.GetListByPager(page, limit);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Insert(EmployEmp employEmp)
        {
            employEmp.onboarding_time = DateTime.Now;
            employEmp.create_time = DateTime.Now;
            employEmp.account_status = 1;
            employEmp.last_log_time = DateTime.Now;
            ResMessage result = _dbService.Add(employEmp);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(EmployEmp employemp)
        {
            employemp.onboarding_time = DateTime.Now;
            employemp.create_time = DateTime.Now;
            employemp.last_log_time = DateTime.Now;
            employemp.account_status = 1;
            ResMessage result = _dbService.Update(employemp);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult del(int id)
        {
            ResMessage res = _dbService.Delete(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getEmp(int id)
        {
            return Json(_dbService.getModel(id), JsonRequestBehavior.AllowGet);
        }
    }
}