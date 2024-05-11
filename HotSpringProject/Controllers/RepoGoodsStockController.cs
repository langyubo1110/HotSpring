using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.DTO;
using HotSpringProject.Entity.VO;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using System;
using System.Collections;
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
        public ActionResult GoodsStock()
        {
            return View();
        }
        public ActionResult Detail(int id=0) 
        {
            ViewBag.id = id;
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _repoGoodsStockService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListByPager(int page, int limit,RepoGoodsStockFilter filter)
        {
            ResMessage resMessage = _repoGoodsStockService.GetListByPager(page,limit,filter);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id = 0)
        {
            ResMessage resMessage = _repoGoodsStockService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateByAudit(RepoGoodsStockDTO repoGoodsStockDTO)
        {
            ResMessage resMessage = _repoGoodsStockService.UpdateByAudit(repoGoodsStockDTO);
            return Json(resMessage);
        }
        public JsonResult GetList(string keywords,int? goods_type)
        {
            ResMessage resMessage = _repoGoodsStockService.GetList(keywords, goods_type);
            if (string.IsNullOrEmpty(keywords)) 
            {
                return Json(resMessage,JsonRequestBehavior.AllowGet);
            }
            //自动补全组件数据格式
            Hashtable ht = new Hashtable();
            ht["code"] = 0;
            ht["msg"] = "";
            ht["data"] = resMessage.data;
            return Json(ht,JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}