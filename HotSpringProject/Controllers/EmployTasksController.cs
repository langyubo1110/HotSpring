using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace HotSpringProject.Controllers
{
    public class EmployTasksController : Controller
    {
        private readonly IGRoomRepairService _db;
        private readonly IEmployPerformService _db1;

        public EmployTasksController(IGRoomRepairService gRoomRepair, IEmployPerformService employPerformService)
        {
            _db = gRoomRepair;
            _db1 = employPerformService;
        }
        /*员工个人表的控制器
        * 刘星宇
        * 2024-05-08
        */
        // GET: EmployTasks
        public ActionResult RepairTasks()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UpKeepTasks() 
        {
            return View();
        }
        public JsonResult Repair()
        {
            int id = 25; 
            ResMessage res = _db.GetListById(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RepairByID(int id=0)
        {
            ResMessage res = _db.GetListId(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult UpKeep() 
        //{

        //}
        public JsonResult Perform(int id =0)
        {
            List<EmployPerform> list = _db1.GetListByRepairId(id);
            if (list.Any())
            {
                return Json(new { code = 200, data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 404, message = "No data found for the provided ID." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}