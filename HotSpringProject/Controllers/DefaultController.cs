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


        public DefaultController(ISystemPageCorrespondenceService systemModuleService, IEmployEmpService db, ISystemPagesService systemPagesService)
        {
            _dbService = systemModuleService;
            _db = db;
            _dbpages = systemPagesService;
        }
        // GET: Default
        #region 页面
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                EmployEmp employEmp = (EmployEmp)Session["User"];
                int role = employEmp.role_id;
                return View(_dbService.GetMenu(role));
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
            string username = Request.Form["txtName"];
            string password = Request.Form["txtPassword"];
            bool isVerify = _db.Verify(username, password);
            if (isVerify)
            {
                var user = _db.GetList().Where(EmployEmp => EmployEmp.name == username).FirstOrDefault();
                ResMessage res= _db.getModel(user.id);
                EmployEmp employEmp = (EmployEmp)res.data;
                Session["User"] = employEmp;
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