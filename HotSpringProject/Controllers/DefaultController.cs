using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HotSpringProjectService;
using System.Reflection;
using HotSpringProjectService.Interface;
using DotNet.Utilities;
using System.Buffers;

using System.Runtime.Remoting.Contexts;
using System.Xml.Linq;
using System.Web.Caching;
using System.Web.UI;
namespace HotSpringProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly ISystemPageCorrespondenceService _dbService;
        private readonly IEmployEmpService _db;
        private readonly ISystemPagesService _dbpages;
        private readonly IEmployRoleService _dbRole;

        public DefaultController(ISystemPageCorrespondenceService systemModuleService, IEmployEmpService db, ISystemPagesService systemPagesService,IEmployRoleService employRoleService)
        {
            _dbService = systemModuleService;
            _db = db;
            _dbpages = systemPagesService;
            _dbRole = employRoleService;
        }
        // GET: Default
        #region 页面
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                EmployEmp employEmp = (EmployEmp)Session["User"];
                int role_id = employEmp.role_id;
                ViewBag.roleName = _dbRole.GetList().Where(x=>x.id == role_id).ToList()[0].role_name;
                ViewBag.name = employEmp.name;
                ViewBag.pip = employEmp.avatar;
                return View(_dbService.GetMenu(role_id));
            }
            else
            {
                return View();  
            }
            
        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult Loginverify()
        {
            string number = Request.Form["txtName"];
            string password = Request.Form["txtPassword"];
            bool isVerify = _db.Verify(number, password);
            if (isVerify)
            {
                var user = _db.GetList().Where(EmployEmp => EmployEmp.job_number == number).FirstOrDefault();
                ResMessage res= _db.getModel(user.id);
                EmployEmp employEmp = (EmployEmp)res.data;
                HttpContext.Session["User"] = employEmp;
                _db.Update(employEmp,true);
                IEnumerable<SystemPages> pages = _dbpages.GetList();
                UserData data = new UserData();
                foreach (var page in pages)
                {
                    int status = _dbService.verify(user.role_id, page.id) ? 1 : 0;
                    data.PageStatus.Add((pageAddress: page.page_address, status: status));
                }
                HttpRuntime.Cache.Insert(user.name, data, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                return RedirectToAction("index", "default");
            }
             else
            {
               return RedirectToAction("login", "default");
            }
            
        }


        #endregion
    }
}