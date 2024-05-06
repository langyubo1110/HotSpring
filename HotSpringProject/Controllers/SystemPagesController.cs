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
    public class SystemPagesController : Controller
    {
        /*页面表的控制器
         * 刘星宇
         * 2024-04-28
         */
        // GET: SystemPages
        private readonly ISystemPagesService _dbService;

        public SystemPagesController(ISystemPagesService systemPagesService)
        {
            _dbService = systemPagesService;
        }
        public ActionResult PagesManager()
        {
            
            return View();
        }
        public ActionResult Detail(int id = 0)
        {
            IEnumerable<(int Id, string Name)> modules = _dbService.GetModuleList();
            ViewBag.id = id;
            return View(modules);
        }
        public JsonResult Pages(PagesFilter filter)
        {
            ResMessage res = _dbService.GetListByPager(filter);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Insert(SystemPages systemPages)
        {
            ResMessage result = _dbService.Add(systemPages);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult Edit(SystemPages systemPages)
        {
            ResMessage result = _dbService.Update(systemPages);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult del(int id)
        {
            ResMessage res = _dbService.Delete(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getPages(int id)
        {
            return Json(_dbService.getModel(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Query(PagesFilter filter)
        {
            return Json(_dbService.GetListByPager(filter), JsonRequestBehavior.AllowGet);
        }
    }
}