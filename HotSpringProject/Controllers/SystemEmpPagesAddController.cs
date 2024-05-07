using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services;

namespace HotSpringProject.Controllers
{
    public class SystemEmpPagesAddController : Controller
    {
        /*权限修改的控制器
         * 刘星宇
         * 2024-04-25
         */
        // GET: SystemEmpPagesAdd
        private readonly IEmployRoleService _dbRole;
        private readonly ISystemPageCorrespondenceService _db;

        public SystemEmpPagesAddController( IEmployRoleService dbRole, ISystemPageCorrespondenceService db)
        {
            _dbRole = dbRole;
            _db = db;
        }
        public ActionResult Index(int role)
        {
            int role1 = role;
            ViewBag.Role = role1;
            return View(_db.GetAllPages(role1));
        }
        public ActionResult RoleList()
        {
            var roles = _dbRole.GetList();
            ViewBag.Roles = roles;
            return View(roles);
        }
        [HttpPost]
        public ActionResult SavePages(int roleId,List<int> selectedPages)
        {
            int role = roleId;

            _db.Delete(role);

            _db.Add(role, selectedPages);

            return Json(new { success = true });
        }

    }
}