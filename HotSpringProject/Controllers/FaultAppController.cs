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
using Newtonsoft.Json.Linq;

namespace HotSpringProject.Controllers
{
    public class FaultAppController : Controller
    {
        private readonly IFaultAppService _faultAppService;

        // GET: FaultApp
        private readonly IRepairTaskReportService _repairTaskReportService;
        private readonly IEquipmentService _equipmentService;
        private readonly IFaultAnalyseService _faultAnalyseService;

        public FaultAppController(IRepairTaskReportService repairTaskReportService, IEquipmentService equipmentService, IFaultAnalyseService faultAnalyseService,IFaultAppService faultAppService)
        {
            _faultAppService = faultAppService;
            _repairTaskReportService = repairTaskReportService;
            _equipmentService = equipmentService;
            _faultAnalyseService = faultAnalyseService;
        }
        //页面
        #region
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int id = 0)
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult RepairTaskReport()
        {
            return View();
        }
       
        #endregion
        //接口
        #region
        public JsonResult List(int page, int limit)
        {
            ResMessage resMessage = _faultAppService.GetList(page, limit);
           
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            bool flag = _faultAppService.Delete(id);
            return flag ? Json(new { code = 200 }) : Json(new { code = 400 });
        }
        public JsonResult GetModel(int id)
        {
            return Json(_faultAppService.GetModel(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpDate(FaultApp faultApp )
        {
            return Json(_faultAppService.UpDate(faultApp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(FaultApp faultApp)
        {
            return Json(_faultAppService.Add(faultApp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult StopAndAdd(int eid, FaultAnalyse faultAnalyse)
        {

            return Json(_faultAppService.StopAndAdd(eid, faultAnalyse), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRepairList( int page, int limit)
        {
            ResMessage result = _faultAppService.GetRepairList(page ,limit);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
      
        #endregion
    }
}