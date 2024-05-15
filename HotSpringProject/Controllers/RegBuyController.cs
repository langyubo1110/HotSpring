using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/*董涵飞
 * 立案表的控制器
 * 2024-05-03
 * 
 */

namespace HotSpringProject.Controllers
{
    public class RegBuyController : Controller
    {
        private readonly IRegApplyService _regApplyService;
        private readonly IRegAuditService _regAuditService;
        private readonly IEmployEmpService _employEmpService;
        private readonly HotSpringDbContext _db;

        public RegBuyController(IRegApplyService regApplyService, IRegAuditService regAuditService,IEmployEmpService employEmpService, HotSpringDbContext db)
        {
            _regApplyService = regApplyService;
            _regAuditService = regAuditService;
            _employEmpService = employEmpService;
            _db = db;   
        }
        // GET: RegBuy
        //弹出层  立案采购表
        public ActionResult RegBuy(int id = 0)
        {
            List<EmployEmp> list=_employEmpService.GetList().ToList();
            ViewBag.Id = id;
            ViewBag.list = list;

            return View();
        }
        //立案申请  主页
        public ActionResult RegApply()
        {
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int userId = employEmp.id;
            
            //Session["userID"] = 22;
            //Session["userName"] = "张三";
            return View();
        }
        #region 接口
        public ActionResult GetList(RegApplyFilter filter)
        {
            return Json(_regApplyService.GetListByPager(filter), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id)
        {
            if (id == 0)
            {
                return Json(new { code = 500, message = "主键不能为空" });
            }
            RegApply regApply = _regApplyService.GetModel(id);
            if (regApply == null)
            {
                return Json(new { code = 500, message = "通过该主键获取的实体为空" });
            }
            return Json(new { code = 200, message = "成功", data = regApply }, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]//关键代码 关闭验证
        public JsonResult Insert(RegApply regapply)
        {
            //接收申请实体
            //调用申请添加方法
            ResMessage res = _regApplyService.AddWithRegAudit(regapply);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Check(int RegId, string txtAdvice)
        {
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int userId = employEmp.id;
            int userID = Convert.ToInt32(userId);
            return Json(_regApplyService.Check(RegId, txtAdvice, userID), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}