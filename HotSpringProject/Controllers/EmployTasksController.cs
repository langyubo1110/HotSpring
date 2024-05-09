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

        public EmployTasksController(IGRoomRepairService gRoomRepair)
        {
            _db = gRoomRepair;
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
            int id = 1; 
            ResMessage res = _db.GetListById(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult UpKeep() 
        //{
            
        //}
    }
}