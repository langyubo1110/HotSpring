
using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/*董涵飞
 * 立案审核表的控制器
 * 2024-04-26上午
 * 
 */

namespace HotSpringProject.Controllers
{
    public class RegAuditController : Controller
    {
        HotSpringDbContext db = new HotSpringDbContext();
        private readonly IRegAuditService _regAuditService;

        public RegAuditController(IRegAuditService regAuditService)
        {
            _regAuditService = regAuditService;
        }
        // GET: Reg_Audit
        public ActionResult Index()
        {

            List<RegAudit> list = _regAuditService.GetList();
            return View();
        }
        public ActionResult test()
        {
            return View();
        }
        public ActionResult Detail(int id = 0)
        {
            //ViewBag.MoviesType = db.regAudit.ToList();
            ViewBag.Id = id;
            return View();
        }
        //接口
        public JsonResult RegAuditList(RegAuditFilter filter)
        {

            #region 哈希表
            //IQueryable<RegAudit> regAudit = db.regAudit;
            //Hashtable hashtable = new Hashtable();
            //hashtable["code"] = 0;
            //hashtable["count"] = regAudit.Count();
            //hashtable["data"] = regAudit.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit);//.Skip((page - 1) * limit).Take(limit)
            //return Json(hashtable, JsonRequestBehavior.AllowGet);
            #endregion
            //ResMessage res = _regAuditService.GetListByPager(page, limit);
            return Json(_regAuditService.GetListByPager(filter), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetModel(int id)
        {
            if (id == 0)
            {
                return Json(new { code = 500, message = "主键不能为空" });
            }
            RegAudit regAudit = _regAuditService.GetModel(id);
            if (regAudit == null)
            {
                return Json(new { code = 500, message = "通过该主键获取的实体为空" });
            }
            return Json(new { code = 200, message = "成功", data = regAudit }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert(RegAudit regAudit)
        {
            
            ResMessage res = _regAuditService.Add(regAudit);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _regAuditService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(RegAudit regAudit)
        {
            ResMessage resMessage = _regAuditService.Update(regAudit);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
    }
}