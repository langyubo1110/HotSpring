using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotSpringProject.Entity;
using HotSpringProjectService;
using HotSpringProjectService.Interface;

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
        public ActionResult Index()
        {
            return View();
        }
    }
}