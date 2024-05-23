using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Windows.Media.Media3D;

namespace HotSpringProject.Controllers
{
    public class RepairRecordController : Controller
    {
        private readonly IRepairRecordService _repairRecordService;

        public RepairRecordController(IRepairRecordService repairRecordService) {
            _repairRecordService=repairRecordService;
        }
        // GET: RepairRecord
        //页面
        #region
        public ActionResult Index(int id=0)
        {
            if (id > 0)
            {
                ViewBag.app_id = id;
            }
            if (Session["User"] != null)
            {
                EmployEmp employEmp = (EmployEmp)Session["User"];
                ViewBag.UserName = employEmp.name;
                ViewBag.id=employEmp.id;
            }
            return View();
        }
        #endregion
        //接口
        #region
        public ActionResult list(int id)
        {

            ResMessage resMessage = _repairRecordService.GetList(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public ActionResult add(RepairRecord repairRecord)
        {
            return Json(_repairRecordService.Add(repairRecord), JsonRequestBehavior.AllowGet);
            
        }
        #endregion
    }
}