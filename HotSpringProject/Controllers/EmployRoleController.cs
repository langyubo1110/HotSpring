using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotSpringProject.Entity;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using DotNet.Utilities;

namespace HotSpringProject.Controllers
{
    /*员工角色的控制器
         * 
         * 2024-04-25
         */
    public class EmployRoleController : Controller
    {
        private readonly IEmployRoleService _employRoleService;

        public EmployRoleController(IEmployRoleService employRoleService) 
        {
            _employRoleService= employRoleService;
        }
        // GET: EmployRole
        #region 页面
        public ActionResult EmployRoleManager()
        {
           
            return View();
        }
        public ActionResult Detail(int id)
        {
            ViewBag.id = id;
            return View();
        }
        #endregion
        #region 接口
        public JsonResult GetRoles()
        {
            ResMessage res = _employRoleService.GetRoles();
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRoleModel(int id)
        {
            return Json(_employRoleService.GetModel(id),JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(EmployRole employRole)
        {
            if (employRole == null)
            {
                return Json(false);
            }
            ResMessage result=_employRoleService.Add(employRole);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            ResMessage res= _employRoleService.Delete(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(EmployRole employRole)
        {
            ResMessage res=_employRoleService.Update(employRole);
            return Json(res);
        }
        #endregion

    }
}