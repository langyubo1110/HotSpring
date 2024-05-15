using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;

namespace HotSpringProject.Controllers
{
    public class EmployEmpController : Controller
    {
        /*员工表的控制器
         * 刘星宇
         * 2024-04-25
         */
        // GET: EmployEmp
        private readonly IEmployEmpService _dbService;
        private readonly IEmployRoleService _dbRole;

        public EmployEmpController(IEmployEmpService employEmpService,IEmployRoleService employRoleService)
        {
            _dbService = employEmpService;
            _dbRole = employRoleService;
        }
        #region 页面
        //员工管理页面
        public ActionResult EmployManager()
        {
            return View();
        }
        //员工注册页面
        public ActionResult EmployRegister()
        {
            return View();
        }
        public ActionResult Detail(int id=0)
        {
            IEnumerable<EmployRole> lsit = _dbRole.GetList();
            ViewBag.list = lsit;
            ViewBag.id = id; 
            return View();
        }
        #endregion
        public JsonResult Employ(EmployEmpFilter filter) 
        {
            ResMessage res = _dbService.GetListByPager(filter);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Insert(EmployEmp employEmp)
        {
            ResMessage result = _dbService.Add(employEmp);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(EmployEmp employemp)
        {

            ResMessage result = _dbService.Update(employemp);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult del(int id)
        {
            ResMessage res = _dbService.Delete(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getEmp(int id)
        {
            return Json(_dbService.getModel(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Query(EmployEmpFilter filter)
        {
            return Json(_dbService.GetListByPager(filter), JsonRequestBehavior.AllowGet);
        }
        public JsonResult img()
        {
            HttpPostedFileBase file = Request.Files[0];
            try
            {
                Directory.CreateDirectory(Server.MapPath("/assets/Aimg"));
                //判断文件夹
                //处理图片名称
                file.SaveAs(Server.MapPath("/Assets/Aimg/") + file.FileName);
                return Json(ResMessage.Success(data: "/Assets/Aimg/" + file.FileName));
            }
            catch (Exception ex)
            {
                return Json(ResMessage.Fail(ex.Message));
            }
        }
    }
}