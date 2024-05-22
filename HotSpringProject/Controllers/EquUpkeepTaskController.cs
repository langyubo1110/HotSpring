using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    
    public class EquUpkeepTaskController : Controller
    {
        private readonly IEquUpkeepTaskService _equUpkeepTaskService;
        private readonly IEmployCheckInService _employCheckInService;

        //构造函数注入
        public EquUpkeepTaskController(IEquUpkeepTaskService equUpkeepTaskService,IEmployCheckInService employCheckInService)
        {
            _equUpkeepTaskService = equUpkeepTaskService;
            _employCheckInService= employCheckInService;
        }

        // GET: EquUpkeepTask
        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        //弹出二维码
        public ActionResult QRimg(int id)
        {
            List<EquUpkeepTask> list = _equUpkeepTaskService.GetTaskList();
            list = list.Where(x => x.equ_plan_id == id).ToList();
            return View(list);
        }
        //弹出当日签到人员
        public ActionResult Sign()
        {
            return View();
        }
        //单个保养任务
        public ActionResult upkeeptask(int id,int equ_id)
        {
            //传planid
            ViewBag.id = id;
            //传equ_id
            ViewBag.equ_id=equ_id;
            List<EquUpkeepTaskVO> list= _equUpkeepTaskService.getlistnofilter();
            list = list.Where(x => x.id== id).ToList();
            return View(list);
        }
        #endregion

        #region 接口
        public JsonResult Query(EquUpkeepTaskFilter filter)
        {
            return Json(_equUpkeepTaskService.GetList(filter), JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkin()
        {
            List<EmployCheckInVO> list = _employCheckInService.GetListUnionSql().ToList();
            list = list.Where(x => x.create_time >= DateTime.Now.Date).ToList();
            return Json(ResMessage.Success(list), JsonRequestBehavior.AllowGet);
        }
        //更新保养任务表数据
        public JsonResult upkeepdistribute(List<EmployCheckInVO> data, int[] equid, int[] equplanid)
        {
            EmployEmp emp = (EmployEmp)Session["User"];
            int empid=emp.id;
            return Json(_equUpkeepTaskService.upkeepdeit(data, equid,equplanid,empid), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}