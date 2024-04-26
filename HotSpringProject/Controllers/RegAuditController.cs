using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class RegAuditController : Controller
    {
        HotSpringDbContext db=new HotSpringDbContext();
        private readonly IRegAuditService _regAuditService;

        public RegAuditController(IRegAuditService regAuditService)
        {
            _regAuditService = regAuditService;
        }
        // GET: Reg_Audit
        public ActionResult Index()
        {
            
            List<RegAudit> list= _regAuditService.GetList();
            return View();
        }
        public ActionResult Detail(int id = 0)
        {
            ViewBag.MoviesType = db.regAudit.ToList();
            ViewBag.Id = id;
            return View();
        }
        //接口
        public JsonResult RegAuditList(int page, int limit/*, string name, string type, string typeId*/)
        {
            #region 哈希表
            //IQueryable<RegAudit> regAudit = db.regAudit;
            //Hashtable hashtable = new Hashtable();
            //hashtable["code"] = 0;
            //hashtable["count"] = regAudit.Count();
            //hashtable["data"] = regAudit.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit);//.Skip((page - 1) * limit).Take(limit)
            //return Json(hashtable, JsonRequestBehavior.AllowGet);
            #endregion
            ResMessage res = _regAuditService.GetListByPager(page, limit);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //public JavaScriptResult GetModel()
        //{

        //}
        public JsonResult Insert(RegAudit reg_Audit)
        {
            ResMessage res=_regAuditService.Add(reg_Audit);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}