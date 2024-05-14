using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class FaultAnalyseController : Controller
    {
        // GET: FaultAnalyse
        private readonly IRepairTaskReportService _repairTaskReportService;
        private readonly IEquipmentService _equipmentService;
        private readonly IFaultAnalyseService _faultAnalyseService;

        public FaultAnalyseController(IRepairTaskReportService repairTaskReportService, IEquipmentService equipmentService, IFaultAnalyseService faultAnalyseService)
        {
           
            _repairTaskReportService = repairTaskReportService;
            _equipmentService = equipmentService;
            _faultAnalyseService = faultAnalyseService;
        }
        public ActionResult Detail(int id=0)
        {
            if(id > 0)
            {
                ViewBag.id=id;
            }
            ViewBag.emList = (List<EmployEmp>)_repairTaskReportService.GetListByRole().data;
            ResMessage resMessage = _repairTaskReportService.GetEquipmentList();
            ViewBag.equipmentNames = resMessage.data;
            return View();
        }
        public JsonResult Add(FaultAnalyse  faultAnalyse)
        {
            return Json(_faultAnalyseService.Add(faultAnalyse), JsonRequestBehavior.AllowGet);
        }

    }
}