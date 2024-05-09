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
    /*2024-05-07
   * 客房备件控制器
   * 裴晨旭
   */
    public class GRoomSparePartsController : Controller
    {
        private readonly IGRoomSparePartsService _gRoomSparePartsService;

        public GRoomSparePartsController(IGRoomSparePartsService gRoomSparePartsService) 
        {
            _gRoomSparePartsService=gRoomSparePartsService;
        }
        #region 页面
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _gRoomSparePartsService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id = 0)
        {
            ResMessage resMessage = _gRoomSparePartsService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(GRoomSpareParts gRoomSpareParts)
        {
            ResMessage resMessage = _gRoomSparePartsService.Add(gRoomSpareParts);
            return Json(resMessage);
        }
        public JsonResult Update(GRoomSpareParts gRoomSpareParts)
        {
            ResMessage resMessage = _gRoomSparePartsService.Update(gRoomSpareParts);
            return Json(resMessage);
        }
        public JsonResult GetList()
        {
            ResMessage resMessage = _gRoomSparePartsService.GetList();
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}