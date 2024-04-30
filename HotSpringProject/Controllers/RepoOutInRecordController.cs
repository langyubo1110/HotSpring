using DotNet.Utilities;
using HotSpringProject.Entity.VO;
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
    /*2024-04-28
     * 商品出入库表控制器
     * 裴晨旭
     */
    public class RepoOutInRecordController : Controller
    {
        private readonly IRepoOutInRecordService _repoOutInRecordService;

        public RepoOutInRecordController(IRepoOutInRecordService repoOutInRecordService) 
        {
            _repoOutInRecordService=repoOutInRecordService;
        }
        #region 页面
        public ActionResult GoodsOutIn()
        {
            return View();
        }
        public ActionResult Detail(int id = 0)
        {
            ViewBag.id = id;
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _repoOutInRecordService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList(int page, int limit, RepoOutInRecordFilter filter)
        {
            ResMessage resMessage = _repoOutInRecordService.GetList(page, limit, filter);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id = 0)
        {
            ResMessage resMessage = _repoOutInRecordService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(RepoOutInRecord repoOutInRecord)
        {
            ResMessage resMessage = _repoOutInRecordService.Add(repoOutInRecord);
            return Json(resMessage);
        }
        public JsonResult Update(RepoOutInRecord repoGoodsStock)
        {
            ResMessage resMessage = _repoOutInRecordService.Update(repoGoodsStock);
            return Json(resMessage);
        }

        #endregion

    }
}