using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class EmployAllsalaryController : Controller
    {
        private readonly IEmployAllsalaryService _employAllsalaryService;

        public EmployAllsalaryController(IEmployAllsalaryService employAllsalaryService) 
        {
            _employAllsalaryService=employAllsalaryService;
        }
        #region 页面
        public ActionResult AllSalary()
        {
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _employAllsalaryService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList()
        {
            ResMessage resMessage = _employAllsalaryService.GetList();
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListByPager(EmployAllsalaryFilter filter)
        {
            ResMessage resMessage = _employAllsalaryService.GetListByPager(filter);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id)
        {
            ResMessage resMessage = _employAllsalaryService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(EmployAllsalary employAllsalary)
        {
            ResMessage resMessage = _employAllsalaryService.Add(employAllsalary);
            return Json(resMessage);
        }
        public JsonResult Update(EmployAllsalary employAllsalary)
        {
            ResMessage resMessage = _employAllsalaryService.Update(employAllsalary);
            return Json(resMessage);
        }
        
        #endregion
    }
}