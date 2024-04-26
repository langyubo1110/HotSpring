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
        #endregion

        #region 接口
        public JsonResult Delete(int id) {
            int flag = _repoGoodsStockService.Delete(id);
            if (flag > 0) 
            { 
            return Json(new { code= 200 , msg = "删除成功" },JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 500 , msg = "删除失败" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList(int page,int limit)
        {
            int count1=0;
            IQueryable<RepoGoodsStock> list = _repoGoodsStockService.GetList();
            count1 = list.Count();
            List<RepoGoodsStock> result=list.OrderBy(x=>x.id).Skip((page-1)*limit).Take(limit).ToList();
            return Json(new { code=0,message="请求成功",count= count1,data= result }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id)
        {
            if (id == 0)
            {
                return Json(new { code = 500, message = "主键不能为空" });
            }
            RepoGoodsStock repoGoodsStock  = _repoGoodsStockService.GetModel(id);
            if (repoGoodsStock == null)
            {
                return Json(new { code = 500, message = "通过该主键获取的实体为空" });
            }
            return Json(new { code = 200, message = "成功", data = repoGoodsStock },JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(RepoGoodsStock repoGoodsStock)
        {
            bool result = _repoGoodsStockService.Add(repoGoodsStock);
            if (result) 
            {
                return Json(new { code = 200, msg = "添加成功" });
            }
            return Json(new { code = 500, msg = "添加失败"});
        }
        public JsonResult Update(RepoGoodsStock repoGoodsStock)
        {
            bool result =  _repoGoodsStockService.Update(repoGoodsStock);
            if (result)
            {
                return Json(new { code = 200, msg = "更新成功" });
            }
            return Json(new { code = 500, msg = "更新失败"});
        }
        #endregion
    }
}