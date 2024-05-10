using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class RegResearchController : Controller
    {
        private readonly IEmployEmpService _employEmpService;
        private readonly IRegEquipResService _regEquipResService;
        private readonly IRegVoteService _regVoteService;

        public RegResearchController(IEmployEmpService employEmpService, IRegEquipResService regEquipResService,IRegVoteService regVoteService)
        {
            _employEmpService = employEmpService;
            _regEquipResService = regEquipResService;
            _regVoteService = regVoteService;
        }
        #region 页面
        // GET: RegResearch
        public ActionResult ReIndex(int id = 0)
        {
            List<EmployEmp> list = _employEmpService.GetList().ToList();
            ViewBag.Id = id;//采购表id
            ViewBag.list = list;
            ViewBag.userName = Session["userName"];
            ViewBag.userId=Session["userId"];
            return View();
        }
        public ActionResult ResAdd(int id=0)
        {
            ViewBag.id = id;//采购表id

            return View();
        }
        public ActionResult ResFile(int id = 0)
        {
           

            return View();
        }
        #endregion

        #region 接口
        //查询调研表的数据
        public ActionResult GetList(int id = 0)
        {
            int voteId = Convert.ToInt32( Session["userId"]);
            ResMessage res = _regEquipResService.GetListById(id,voteId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //去查询数据出来 厂商：xxx水厂：张三 李四
        public ActionResult GetData(int id=0)
        {
            ResMessage res = _regEquipResService.GetListForBind(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Fileimg(int id)
        {
            HttpPostedFileBase file = Request.Files[0];
            try
            {
                if (Directory.Exists(Server.MapPath("/assets/upload/research/") + id) == false)
                {
                    //判断文件夹
                    Directory.CreateDirectory(Server.MapPath("/assets/upload/research/") + id);
                }
                //处理图片名称
                file.SaveAs(Server.MapPath($"/assets/upload/research/{id}/") + file.FileName);
                string type = ".jpg";
                if (file.FileName.Contains(type))
                {
                    return Json(ResMessage.Success(data: $"/assets/upload/research/{id}/" + file.FileName));
                }
                else
                    return Json(ResMessage.Success());
            }
            catch (Exception ex)
            {
                return Json(ResMessage.Fail(ex.Message));
            }
            
        }
        //resadd插入调研表
        public ActionResult Insert(RegEquipRes regEquipRes)
        {
            ResMessage res = _regEquipResService.Add(regEquipRes);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //index页更改投票信息
        public ActionResult VoteUpdate(int resId = 0,int regId=0)
        {
            int userID = Convert.ToInt32(Session["userID"]);
            ResMessage res = _regVoteService.UpdateWithVote(regId, resId, userID);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}