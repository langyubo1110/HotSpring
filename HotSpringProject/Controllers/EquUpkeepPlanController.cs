using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    
    public class EquUpkeepPlanController : Controller
    {
        private readonly IEquUpkeepPlanService _equUpkeepPlanService;
        private readonly IEquipmentService _equipmentService;

        //构造函数注入
        public EquUpkeepPlanController(IEquUpkeepPlanService equUpkeepPlanService,IEquipmentService equipmentService)
        {
            _equUpkeepPlanService = equUpkeepPlanService;
            _equipmentService = equipmentService;
        }

        // GET: EquUpkeepPlan
        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult detail(int id=0)
        {
            ViewBag.equid = _equipmentService.getList();
            ViewBag.Id = id;
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Query(EquUpkeepPlanFilter filter)
        {
            return Json(_equUpkeepPlanService.GetListUnion(filter), JsonRequestBehavior.AllowGet);
        }
        //表单赋值
        public JsonResult getequip(int id)
        {
            return Json(_equUpkeepPlanService.GetModel(id), JsonRequestBehavior.AllowGet);
        }
        //添加
        public JsonResult add(EquUpkeepPlan keepplan)
        {
            return Json(_equUpkeepPlanService.Add(keepplan), JsonRequestBehavior.AllowGet);
        } 
        //更新
        public JsonResult edit(EquUpkeepPlan keepplan)
        {
            return Json(_equUpkeepPlanService.Update(keepplan), JsonRequestBehavior.AllowGet);
        } 
        //删除
        public JsonResult delete(int id)
        {
            return Json(_equUpkeepPlanService.Delete(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}