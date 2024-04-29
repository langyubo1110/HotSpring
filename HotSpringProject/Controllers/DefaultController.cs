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
namespace HotSpringProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly  ISystemModuleService _dbService;

        public DefaultController(ISystemModuleService systemModuleService)
        {
            _dbService = systemModuleService;
        }
        // GET: Default
        #region 页面
        public ActionResult Index()
        {
            List<SystemModule> list = _dbService.GetList();
            return View(list);
        }
        public ActionResult Login()
        {
            return View();
        }
        #endregion
    }
}