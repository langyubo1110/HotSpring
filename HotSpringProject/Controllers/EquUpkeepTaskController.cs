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
    
    public class EquUpkeepTaskController : Controller
    {
        private readonly IEquUpkeepTaskService _equUpkeepTaskService;

        //构造函数注入
        public EquUpkeepTaskController(IEquUpkeepTaskService equUpkeepTaskService)
        {
            _equUpkeepTaskService = equUpkeepTaskService;
        }

        // GET: EquUpkeepTask
        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        //弹出二维码
        public ActionResult QRimg(int id)
        {
            List<EquUpkeepTaskVO> list = _equUpkeepTaskService.getlistnofilter();
            list = list.Where(x => x.id == id).ToList();
            return View(list);
        }
        //弹出当日签到人员
        public ActionResult Sign()
        {
            return View();
        }
        //单个保养任务
        public ActionResult upkeeptask(int id)
        {
            List<EquUpkeepTaskVO> list= _equUpkeepTaskService.getlistnofilter();
            list = list.Where(x => x.id== id).ToList();
            return View(list);
        }
        #endregion

        #region 接口
        public JsonResult Query(EquUpkeepTaskFilter filter)
        {
            return Json(_equUpkeepTaskService.GetList(filter), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}