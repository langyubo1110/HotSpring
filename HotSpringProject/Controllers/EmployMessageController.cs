using DotNet.Utilities;
using HotSpringProject.DependencyDB;
using HotSpringProject.Entity;
using HotSpringProject.hubs;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class EmployMessageController : Controller
    {
        private int userId=0;
        private readonly IEmployEmpService _employEmpService;
        private readonly IEmployRoleService _employRoleService;
        private readonly IEmployMessageService _employMessage;

        public EmployMessageController(IEmployEmpService employEmpService,IEmployRoleService employRoleService,IEmployMessageService employMessageService)
        {
            _employEmpService = employEmpService;
            _employRoleService = employRoleService;
            _employMessage = employMessageService;
        }
        // GET: Chat 站内信
        public ActionResult MesIndex()
        {
            return View();
        }
        public ActionResult MesWrite()
        {
            List<EmployEmp> list = _employEmpService.GetList().ToList();
            List<EmployRole> roleList = _employRoleService.GetList().ToList();
            ViewBag.list = list;
            ViewBag.roleList = roleList;
            EmployEmp user = (EmployEmp)HttpContext.Session["User"];
            ViewBag.userId = user.id;
            return View();
        }
        public ActionResult MesHistory()
        {
            return View();
        }
        public ActionResult GetNumber()
        {
            EmployEmp user = (EmployEmp)HttpContext.Session["User"];
            userId = user.id;
            var messageService = new GetMessageDepency(userId);
            List<EmployMessageVO> messageList = messageService.GetMessage();
            var list1 = messageList.Where(s => s.state == 0).ToList();
            return Content(list1.Count().ToString());
        }
        public JsonResult GetList()
        {
            EmployEmp user = (EmployEmp)HttpContext.Session["User"];
            userId = user.id;
            var messageService = new GetMessageDepency(userId);
            List<EmployMessageVO> messageList = messageService.GetMessage();
            return Json(messageList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendList()
        {
            EmployEmp user = (EmployEmp)HttpContext.Session["User"];
            userId = user.id;
            var messageService = new SendMessageDepency(userId);
            List<EmployMessageVO> messageList = messageService.SendMessage();
            return Json(messageList, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public JsonResult Insert(EmployMessageVO employMessagevo)
        {
            ResMessage res = _employMessage.Add(employMessagevo);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Read(int id)
        {
            ResMessage res = _employMessage.Read(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}