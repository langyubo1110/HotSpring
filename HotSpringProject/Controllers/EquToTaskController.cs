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
    public class EquToTaskController : Controller
    {
        private readonly IEquToTaskService _equToTaskService;

        //构造函数注入
        public EquToTaskController(IEquToTaskService equToTaskService)
        {
            _equToTaskService = equToTaskService;
        }
            
        public JsonResult Query(int id, EquUpkeepPlanFilter filter)
        {
            return Json(_equToTaskService.GetList(id,filter), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddAfterDelete(List<EquToTaskVO> getData, int id, EquUpkeepPlanFilter filter)
        {
            return Json(_equToTaskService.AddAfterDelete(getData, filter, id), JsonRequestBehavior.AllowGet);
        }

    }
}