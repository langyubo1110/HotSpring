using DotNet.Utilities;
using HotSpringProject.Entity.VO;
using HotSpringProject.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotSpringProjectService.Interface;
using HotSpringProjectService;

namespace HotSpringProject.Controllers
{
    /*2024-05-07
     * 客房维修上报控制器
     * 裴晨旭
     */
    public class GRoomRepairController : Controller
    {
        private readonly IGRoomRepairService _gRoomRepairService;
        private readonly IEmployEmpService _employEmpService;

        public GRoomRepairController(IGRoomRepairService gRoomRepairService,IEmployEmpService employEmpService) 
        {
            _gRoomRepairService=gRoomRepairService;
            _employEmpService=employEmpService;
        }
        #region 页面
        public ActionResult GRoomRepair()
        {
            List<EmployEmp> list = _employEmpService.GetList().ToList();
            ViewBag.list = list;
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _gRoomRepairService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id = 0)
        {
            ResMessage resMessage = _gRoomRepairService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(GRoomRepair gRoomRepair,List<GRoomSparePartsVO> gRoomSPVOlist)
        {
            ResMessage resMessage = _gRoomRepairService.Add(gRoomRepair, gRoomSPVOlist);
            return Json(resMessage);
        }
        public JsonResult Update(GRoomRepair gRoomRepair)
        {
            ResMessage resMessage = _gRoomRepairService.Update(gRoomRepair);
            return Json(resMessage);
        }
        public JsonResult GetList()
        {
            ResMessage resMessage = _gRoomRepairService.GetList();
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}