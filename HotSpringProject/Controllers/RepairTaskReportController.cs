using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
namespace HotSpringProject.Controllers
{
    public class RepairTaskReportController : Controller
    {
        private readonly IRepairTaskReportService _repairTaskReportService;
        private readonly IEquipmentService _equipmentService;

        public RepairTaskReportController(IRepairTaskReportService  repairTaskReportService, IEquipmentService equipmentService) {
            _repairTaskReportService = repairTaskReportService;
            _equipmentService=equipmentService;
        }
        //private readonly 
        // GET: RepairTaskReport
        
        #region 页面
        public ActionResult Index()
        {
            //ViewBag.EquipName =_equipmentService.
            return View();
        }
        public ActionResult Details(int id=0)
        {
            ViewBag.id = id;
            return View( );
        }
        
        public ActionResult RepairTaskReport(int id)
        {
            
            
            ViewBag.equipmentNames = _repairTaskReportService.GetEqumentName(id);

            return View( );
        }
        //维修任务列表页
        public ActionResult RepairTaskList()
        {
            return View();
        }
        #endregion
        #region 接口

        public JsonResult List(int page, int limit)
        {
            ResMessage resMessage = _repairTaskReportService.GetList(page,limit);
            return Json(resMessage,JsonRequestBehavior.AllowGet);

        }
        public JsonResult Delete(int id)
        {
            bool flag=_repairTaskReportService.Delete(id);
            return flag ? Json(new { code =200 }) : Json(new { code =400 });
        }
        public JsonResult GetModel(int id)
        {
            return Json(_repairTaskReportService.GetModel(id));
        }
        public JsonResult Update(RepaieTaskReport repaieTaskReport)
        {
            return Json(_repairTaskReportService.UpDate(repaieTaskReport));
        }
        public JsonResult Add(RepaieTaskReport repaieTaskReport)
        {
            return Json(_repairTaskReportService.Add(repaieTaskReport));
        }
        //更新设备状态接口
        //public JsonResult UpdateEquipmentStatus(int id)
        //{
        //  bool flag = _equipmentService.UpdateEquipmentStatus(id);
        //  return flag ? Json(new { code = 200 }) : Json(new { code = 400 });
        //}

        #endregion
    }
}