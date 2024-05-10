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
using HotSpringProject.Entity.DTO;

namespace HotSpringProject.Controllers
{
    /*2024-04-28
     * 商品出入库表控制器
     * 裴晨旭
     */
    public class RepoOutInRecordController : Controller
    {
        private readonly IRepoOutInRecordService _repoOutInRecordService;
        private readonly IRepoGoodsStockService _repoGoodsStockService;
        private readonly IEmployEmpService _employEmpService;

        public RepoOutInRecordController(IRepoOutInRecordService repoOutInRecordService,IRepoGoodsStockService repoGoodsStockService,IEmployEmpService employEmpService) 
        {
            _repoOutInRecordService=repoOutInRecordService;
            _repoGoodsStockService=repoGoodsStockService;
            _employEmpService=employEmpService;
        }
        #region 页面
        public ActionResult GoodsOutIn()
        {
            return View();
        }
        public ActionResult OutDetail()
        {
            List<RepoGoodsStock> goodslist = (List<RepoGoodsStock>)_repoGoodsStockService.GetList(null,null).data;
            ViewBag.goodslist = goodslist;
            List<EmployEmp> employlist = _employEmpService.GetList().ToList();
            ViewBag.employlist = employlist;
            return View();
        }
        public ActionResult InDetail()
        {
            List<EmployEmp> employlist = _employEmpService.GetList().ToList();
            ViewBag.list = employlist;
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _repoOutInRecordService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList()
        {
            ResMessage resMessage = _repoOutInRecordService.GetList();
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id = 0)
        {
            ResMessage resMessage = _repoOutInRecordService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(RepoGoodsStockDTO repoGoodsStockDTO)
        {
            ResMessage resMessage = _repoOutInRecordService.Add(repoGoodsStockDTO);
            return Json(resMessage);
        }
        public JsonResult Update(RepoOutInRecord repoGoodsStock)
        {
            ResMessage resMessage = _repoOutInRecordService.Update(repoGoodsStock);
            return Json(resMessage);
        }
        public JsonResult GetListBySql(int? page, int? limit,RepoOutInRecordFilter filter) 
        {
            ResMessage resMessage = _repoOutInRecordService.GetListBySql(page,limit,filter);
            return Json(resMessage,JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}