using DotNet.Utilities;
using HotSpringProject.Entity.VO;
using HotSpringProject.Entity;
using HotSpringProject.Entity.DTO;
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
            EmployEmp emp = (EmployEmp)Session["User"];
            ViewBag.id = emp.id;
            ViewBag.name = emp.name;
            List<RepoGoodsStock> goodslist = (List<RepoGoodsStock>)_repoGoodsStockService.GetList(null,null).data;
            ViewBag.goodslist = goodslist;
            List<EmployEmp> employlist = _employEmpService.GetList().ToList();
            ViewBag.employlist = employlist;
            return View();
        }
        public ActionResult InDetail()
        {
            EmployEmp emp = (EmployEmp)Session["User"];
            ViewBag.id = emp.id;
            ViewBag.name = emp.name;
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
        //出库人当天出库多种备件
        //获取出库人使用备件的链表（商品名称，出库数量）
        public JsonResult GetListBySpareParts(RepoOutInRecordFilter filter)
        {
            ResMessage resMessage = _repoOutInRecordService.GetListBySpareParts(filter);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id = 0)
        {
            ResMessage resMessage = _repoOutInRecordService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(RepoGoodsStockDTO repoGoodsStockDTO)
        {
            EmployEmp user = (EmployEmp)Session["User"];
            int userId = user.id;
            ResMessage resMessage = _repoOutInRecordService.Add(repoGoodsStockDTO,userId);
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
        //入库时商品图片上传
        public JsonResult UpLoad()
        {
            HttpPostedFileBase file = Request.Files[0];
            string filename = file.FileName;
            string path = Server.MapPath("/assets/goods/images");
            ResMessage resMessage = _repoOutInRecordService.UpLoad(filename,path,file);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}