using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class EmployTasksController : Controller
    {
        private readonly IGRoomRepairService _db;
        private readonly IEmployPerformService _db1;
        private readonly IEmployTasksService _dbTasks;
        private readonly IEquUpkeepTaskService _dbEqu;

        public EmployTasksController(IGRoomRepairService gRoomRepair, IEmployPerformService employPerformService,IEmployTasksService employTasksService, IEquUpkeepTaskService equUpkeepTaskService)
        {
            _db = gRoomRepair;
            _db1 = employPerformService;
            _dbTasks = employTasksService;
            _dbEqu = equUpkeepTaskService;
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
        public ActionResult QRimg(int planid,int equid)
        {
            List<EquUpkeepTask> list = _dbEqu.GetTaskList();
            list = list.Where(x => x.equ_plan_id == planid && x.equ_id==equid).ToList();
            return View(list);
        }
        public ActionResult UpKeepTasks() 
        {
            return View();
        }
        public JsonResult Repair(string yyyy_MM,int page=0,int limit = 0)
        {
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int id = employEmp.id;
            ResMessage res = _db.GetListById( yyyy_MM, id, page, limit);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RepairByID(int id=0)
        {
            ResMessage res = _db.GetListId(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpKeep()
        {
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int id = employEmp.id;
            DateTime today = DateTime.Today;
            IEnumerable<EquUpkeepTaskVO> list = _dbTasks.GetList(id).Where(x => x.upkeep_time.HasValue && x.upkeep_time.Value.Date == today);
            if (list.Any())
            {
                return Json(new { code = 200, data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 404, message = "No data found for the provided ID." }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpKeepHistory()
        {
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int id = employEmp.id;
            DateTime today = DateTime.Today;
            IEnumerable<EquUpkeepTaskVO> list = _dbTasks.GetList(id).Where(x => x.upkeep_time.HasValue && x.upkeep_time.Value.Date < today);
            if (list.Any())
            {
                return Json(new { code = 200, data = list }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 404, message = "No data found for the provided ID." }, JsonRequestBehavior.AllowGet);
            }
        }
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

        [ValidateInput(false)]
        public JsonResult UpdateTaskinfo(int plan_id, string data1,int equ_id)
        {
            ResMessage res = _dbTasks.UpdateTask(plan_id, data1,equ_id);
            return Json(res);
        }
    }
}