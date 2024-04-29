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

        public RepairTaskReportController(IRepairTaskReportService  repairTaskReportService) {
            _repairTaskReportService = repairTaskReportService;
        }
        //private readonly 
        // GET: RepairTaskReport
        
        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id=0)
        {
            ViewBag.id = id;
            return View( );
        }
        
        public ActionResult RepairTaskReport()
        {
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
            ResMessage resMessage = _repairTaskReportService.getlist(page,limit);
            return Json(resMessage,JsonRequestBehavior.AllowGet);

        }
        public JsonResult Delete(int id)
        {
            bool flag=_repairTaskReportService.delete(id);
            return flag ? Json(new { code =200 }) : Json(new { code =400 });
        }
        public JsonResult GetModel(int id)
        {
            return Json(_repairTaskReportService.getmodel(id));
        }
        public JsonResult Update(RepaieTaskReport repaieTaskReport)
        {
            return Json(_repairTaskReportService.update(repaieTaskReport));
        }
        public JsonResult Add(RepaieTaskReport repaieTaskReport)
        {
            return Json(_repairTaskReportService.add(repaieTaskReport));
        }
        //public JsonResult UpdateStatus(int id)
        //{
        //    return Json(_)        
        //}
        #endregion
    }
}