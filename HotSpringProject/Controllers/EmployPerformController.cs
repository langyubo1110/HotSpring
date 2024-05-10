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
    /*2024-05-09
     * 绩效表控制器
     * 裴晨旭
     */
    public class EmployPerformController : Controller
    {
        private readonly IEmployPerformService _employPerformService;

        public EmployPerformController(IEmployPerformService employPerformService) 
        {
            _employPerformService=employPerformService;
        }
        #region 页面
        public ActionResult Perform()
        {
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _employPerformService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList()
        {
            ResMessage resMessage = _employPerformService.GetList();
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id)
        {
            ResMessage resMessage = _employPerformService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(EmployPerform employPerform)
        {
            ResMessage resMessage = _employPerformService.Add(employPerform);
            return Json(resMessage);
        }
        public JsonResult Update(EmployPerform employPerform)
        {
            ResMessage resMessage = _employPerformService.Update(employPerform);
            return Json(resMessage);
        }
        #endregion
    }
}