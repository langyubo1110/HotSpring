    using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
        private readonly IRegFileService _regFileService;
        private readonly IEquipmentService _equipmentService;

        public RegResearchController(IEmployEmpService employEmpService, IRegEquipResService regEquipResService, IRegVoteService regVoteService, IRegFileService regFileService, IEquipmentService equipmentService)
        {
            _employEmpService = employEmpService;
            _regEquipResService = regEquipResService;
            _regVoteService = regVoteService;
            _regFileService = regFileService;
            _equipmentService = equipmentService;
        }
        #region 页面
        // GET: RegResearch
        public ActionResult ReIndex(int id = 0)
        {
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int userId = employEmp.id;
            string userName = employEmp.name;
            List<EmployEmp> list = _employEmpService.GetList().ToList();
            ViewBag.Id = id;//采购表id
            ViewBag.list = list;
            ViewBag.userName = userName;
            ViewBag.userId = userId;
            return View();
        }
        public ActionResult ResAdd(int id = 0)
        {
            ViewBag.id = id;//采购表id

            return View();
        }
        public ActionResult ResFile(int id = 0)
        {
            ViewBag.id = id;

            return View();
        }
        public ActionResult ResBuy(string name, int resid = 0)
        {
            ViewBag.name = name;
            ViewBag.resid = resid;
            return View();
        }
        #endregion

        #region 接口
        //查询调研表的数据
        public ActionResult GetList(int id = 0)
        {
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int userId = employEmp.id;
            int voteId = Convert.ToInt32(userId);
            ResMessage res = _regEquipResService.GetListById(id, voteId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //去查询数据出来 厂商：xxx水厂：张三 李四
        public ActionResult GetData(int id = 0)
        {
            ResMessage res = _regEquipResService.GetListForBind(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //查对应文件全表
        public ActionResult GetFile(int id = 0)//采购表id
        {
            ResMessage res = _regFileService.GetListForFile(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //单个文件下载接口
        public JsonResult DownLoading(RegFileVO data)
        {

            try
            {
                if (Directory.Exists(Server.MapPath($"/assets/download/{data.id}")) == false)
                {
                    //判断文件夹
                    Directory.CreateDirectory(Server.MapPath($"/assets/download/{data.id}"));
                }
                // 打开源文件
                using (FileStream sourceStream = new FileStream(Server.MapPath(data.file_path), FileMode.Open, FileAccess.Read))
                {
                    // 创建目标文件，并且使用 FileMode.CreateNew 来确保如果文件已存在会抛出异常
                    using (FileStream destinationStream = new FileStream(Server.MapPath($"/assets/download/{data.id}/{data.file_path_name}"), FileMode.CreateNew, FileAccess.Write))
                    {
                        // 使用 Stream 的 CopyTo 方法来执行文件复制操作
                        sourceStream.CopyTo(destinationStream);
                    }
                }

                // 文件复制成功
                return Json(ResMessage.Fail($"文件下载成功"));
            }
            catch (Exception ex)
            {
                // 捕获异常，并输出异常信息
                return Json(ResMessage.Fail($"文件保存失败：{ex.Message}"));
            }
        }
        public ActionResult DownloadSingle(RegFileVO data)
        {
            // 文件路径
            string filePath = (Server.MapPath(data.file_path) );
            // 写入数据到文件
            try
            {
                string type = data.file_path_name.Substring(data.file_path_name.LastIndexOf('.') + 1);
                // 在此处生成你要下载的文件，或者从某个位置读取文件
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(data.file_path) );
                // 将 txt 文件发送给客户端进行下载
                return File(fileBytes, $"application/{type}", $"{data.file_path_name}");
            }
            catch (Exception ex)
            {
                return Json(ResMessage.Fail($"{ex.Message}"));
            }

        }
        //批量下载
        public ActionResult DownSelect(List<RegFile> data)
        {
            List<string> filesToCompress = new List<string>();
            //获取当前guid避免id重复
            string guid = Guid.NewGuid().ToString();

            //循环遍历勾选的行数据
            foreach (var item in data)
            {
                // 文件路径
                string filePath = (Server.MapPath(item.file_path));

                filesToCompress.Add(filePath);
            }
            string zipFileName = (Server.MapPath("/assets/download/") + $"{guid}.zip");
            //string zipname = $"{guid}.zip";
            // 创建一个新的ZIP文件
            using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
            {
                foreach (string file in filesToCompress)
                {
                    // 将每个文件添加到ZIP存档中
                    string entryName = Path.GetFileName(file);
                    archive.CreateEntryFromFile(file, entryName);
                }
            }
            // 在此处生成你要下载的文件，或者从某个位置读取文件
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("/assets/download/") + $"{guid}.zip");
            // 将 ZIP 文件发送给客户端进行下载
            return File(fileBytes, "application/zip", "files.zip");
        }




        //文件批量上传
        public ActionResult Fileimg(int id)//采购表id
        {
            HttpPostedFileBase file = Request.Files[0];
            try
            {
                if (Directory.Exists(Server.MapPath("/assets/upload/research/") + id) == false)
                {
                    //判断文件夹
                    Directory.CreateDirectory(Server.MapPath("/assets/upload/research/") + id);
                }
                //向文件表里插入数据
                string filepath = $"/assets/upload/research/{id}/" + file.FileName;
                _regFileService.AddWithId(id, filepath);

                //处理图片名称
                file.SaveAs(Server.MapPath(filepath));
                string type = ".jpg.png.jpeg.gif";
                string fileType =file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
                if (type.Contains(fileType))
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
        [ValidateInput(false)]//关键代码 关闭验证
        public ActionResult Insert(RegEquipRes regEquipRes)
        {
            ResMessage res = _regEquipResService.Add(regEquipRes);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        //index页更改投票信息
        public ActionResult VoteUpdate(int resId = 0, int regId = 0)
        {
            EmployEmp employEmp = (EmployEmp)Session["User"];
            int userId = employEmp.id;
            ResMessage res = _regVoteService.UpdateWithVote(regId, resId, userId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EquipAdd(Equipment Equipment)
        {
            ResMessage res = _equipmentService.AddWithRes(Equipment);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}