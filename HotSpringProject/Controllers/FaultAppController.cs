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
        public ActionResult Detail(int id=0)
        {
            if (Session["User"] != null)
            {
                EmployEmp employEmp = (EmployEmp)Session["User"];
                ViewBag.UserName = employEmp.name;
            }
            //ViewBag.emList = (List<EmployEmp>)_repairTaskReportService.GetListByRole().data;
            if (id!=0)
            {
                ViewBag.id=id;
            }
            ResMessage resMessage = _repairTaskReportService.GetEquipmentList();
            ViewBag.equipmentNames = resMessage.data;
            return View();
        }
        public ActionResult RepairTaskReport()
        {
            if (Session["User"]!=null)
            {

                EmployEmp employEmp = (EmployEmp)Session["User"];
                ViewBag.UserName=employEmp.name;
                ViewBag.ID=employEmp.id;
            }
            return View();
        }
        
        public ActionResult FaultAnalyseAudit(int id=0)
        {
            if (id > 0)
            {
                ViewBag.id=id;
            }
            ResMessage resmessage = _faultAppService.GetAnalyseContents(id);
            ViewBag.contents=resmessage.data;
            return View();
        }
        //审核记录
        public ActionResult Record(int id=0)
        {
            if (id > 0)
            {
                ViewBag.id = id;
            }
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
        //故障申请表添加一条数据，故障分析表添加N条数据
        public JsonResult Add(FaultApp faultApp)
        {   
           
            return Json(_faultAppService.Add(faultApp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult StopAndAdd(int eid, FaultAnalyse faultAnalyse)
        {

            return Json(_faultAppService.StopAndAdd(eid, faultAnalyse), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRepairList( string name,int page, int limit,int? fault_app_id=null)
        {
            ResMessage result = _faultAppService.GetRepairList(name,page ,limit,fault_app_id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAnalyseContents(int id)
        {
            
            return Json(_faultAppService.GetAnalyseContents(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}