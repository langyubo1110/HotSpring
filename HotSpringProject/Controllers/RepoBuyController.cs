using DotNet.Utilities;
using HotSpringProject.Entity.VO;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotSpringProjectService.Interface;
using HotSpringProjectService;
using System.Security.Policy;
using HotSpringProject.Entity.DTO;

namespace HotSpringProject.Controllers
{
    public class RepoBuyController : Controller
    {
        private readonly IRepoBuyService _repoBuyService;
     
        public RepoBuyController(IRepoBuyService repoBuyService) 
        {
            _repoBuyService=repoBuyService;
        }
        #region 页面
        //库存页详情按钮
        //采购详情页
        public ActionResult Detail(int id=0)
        {
            ViewBag.id=id;
            return View();
        }
        #endregion
        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _repoBuyService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList(int id=0)
        {
            ResMessage resMessage = _repoBuyService.GetList(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListBypager(int page,int limit,int id = 0)
        {
            ResMessage resMessage = _repoBuyService.GetListByPager(page,limit,id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id=0)
        {
            ResMessage resMessage = _repoBuyService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(RepoBuy repoBuy)
        {
            ResMessage resMessage = _repoBuyService.Add(repoBuy);
            return Json(resMessage);
        }
        public JsonResult Update(RepoBuy repoBuy)
        {
            ResMessage resMessage = _repoBuyService.Update(repoBuy);
            return Json(resMessage);
        }

        #endregion
    }
}