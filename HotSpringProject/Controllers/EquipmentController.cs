using dbs.Utils;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


/*  郎于博
 *  2024-04-25
 *  设备控制器
 */
namespace HotSpringProject.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;

        //构造函数注入
        public EquipmentController (IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult detail(int id=0)
        {
            //ViewBag.equipment = _equipmentService.GetModel(id);
            ViewBag.Id = id;
            return View();
        }
        #endregion
        
        
        #region 接口
        public JsonResult Query(int page, int limit )
        {
            return Json(_equipmentService.GetListByPager(page, limit), JsonRequestBehavior.AllowGet);
        }

        //删除
        public JsonResult delete(int id)
        {
            return Json(_equipmentService.Delete(id),JsonRequestBehavior.AllowGet);
        }

        //添加
        public JsonResult add(Equipment equipment)
        {
            return Json(_equipmentService.Add(equipment),JsonRequestBehavior.AllowGet);
        }

        //表单赋值
        public JsonResult getequip(int id)
        {
            
            return Json(_equipmentService.GetModel(id),JsonRequestBehavior.AllowGet);
        }
        //编辑
        public JsonResult edit(Equipment equipment)
        {
            return Json(_equipmentService.Update(equipment),JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}