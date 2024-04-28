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
    public class RepoGoodsStockController : Controller
    {
        private readonly IRepoGoodsStockService _repoGoodsStockService;

/* 商品库存表控制器
* 裴晨旭
* 2024-04-25
*/
        public RepoGoodsStockController(IRepoGoodsStockService repoGoodsStockService) {
            _repoGoodsStockService=repoGoodsStockService;
        }
        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int id=0) 
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult test() { 
            return View();  
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id) {
            ResMessage resMessage = _repoGoodsStockService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList(int page,int limit)
        {
            ResMessage resMessage =_repoGoodsStockService.GetList(page, limit);
            return Json(resMessage,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id=0)
        {
            ResMessage resMessage = _repoGoodsStockService.GetModel(id);
            return Json(resMessage,JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(RepoGoodsStock repoGoodsStock)
        {
           ResMessage resMessage = _repoGoodsStockService.Add(repoGoodsStock);
            return Json(resMessage);
        }
        public JsonResult Update(RepoGoodsStock repoGoodsStock)
        {
          ResMessage resMessage = _repoGoodsStockService.Update(repoGoodsStock);
            return Json(resMessage);
        }
        #endregion
    }
}