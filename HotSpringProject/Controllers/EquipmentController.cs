using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;
using HotSpringProjectService;
using System.Security.Policy;
using System.Net.Http;
using System.Threading.Tasks;
using HotSpringProject.Entity.VO;
using System.Text;

/*  郎于博
 *  2024-04-25
 *  设备控制器
 */
namespace HotSpringProject.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IEquToTaskService _equToTaskService;
        private readonly IEquUpkeepTaskService _equUpkeepTaskService;




        //构造函数注入
        public EquipmentController(IEquipmentService equipmentService, IEquToTaskService equToTaskService, IEquUpkeepTaskService equUpkeepTaskService)
        {
            _equipmentService = equipmentService;
            _equToTaskService = equToTaskService;
            _equUpkeepTaskService = equUpkeepTaskService;
        }

        #region 页面
        public ActionResult Index()
        {
            ViewBag.EquipType = _equipmentService.getTypeList();
            return View();
        }
        public ActionResult detail(int id = 0)
        {
            ViewBag.EquipType = _equipmentService.getTypeList();
            ViewBag.Id = id;
            return View();
        }
        public ActionResult upload(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public ActionResult download()
        {
            return View();
        }
        public ActionResult upkeep(int id)
        {
            List<EquipmentTypeVO> list = _equipmentService.GetListUnion().ToList();
            list = list.Where(x => x.id == id).ToList();
            list[0].sname = list[0].status == 0 ? "未启用" : "已启用";
            return View(list);
        }
        #endregion


        #region 接口
        public JsonResult Query(EquipmentFilter filter)
        {
            return Json(_equipmentService.GetListByPager(filter), JsonRequestBehavior.AllowGet);
        }
        public JsonResult equtotask()
        {
            List<EquUpkeepTaskVO> list1 = _equUpkeepTaskService.getlistnofilter();
            return Json(list1, JsonRequestBehavior.AllowGet);
        }

        //文件上传接口
        public JsonResult uploading(int id)
        {
            HttpPostedFileBase file = Request.Files[0];
            try
            {
                //判断文件夹
                Directory.CreateDirectory(Server.MapPath("/assets/upload/") + id);
                //处理图片名称
                file.SaveAs(Server.MapPath($"/assets/upload/{id}/") + file.FileName);
                return Json(ResMessage.Success(data: "/assets/upload/" + file.FileName));
            }
            catch (Exception ex)
            {
                return Json(ResMessage.Fail(ex.Message));
            }
        }
        //文件下载接口
        public ActionResult downloading(EquipmentTypeVO data)
        {
            // 文件路径
            string filePath = (Server.MapPath("/assets/download/") + $" {data.id}.txt");
            // 写入数据到文件
            try
            {
                DataChange(filePath, data);
                // 在此处生成你要下载的文件，或者从某个位置读取文件
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                // 将 txt 文件发送给客户端进行下载
                return File(fileBytes, "application/txt", "files.txt");
            }
            catch (Exception ex)
            {
                return Json(ResMessage.Fail($"{ex.Message}"));
            }
        }

        //文件多选下载接口
        public ActionResult downselect(List<EquipmentTypeVO> data)
        {
            List<string> filesToCompress = new List<string>();
            //获取当前guid避免id重复
            string guid = Guid.NewGuid().ToString();

            //循环遍历勾选的行数据
            foreach (var item in data)
            {
                // 文件路径
                string filePath = (Server.MapPath($"/assets/download/") + $"{item.id}.txt");
                // 写入数据到文件
                DataChange(filePath, item);
                filesToCompress.Add(filePath);

            }
            string zipFileName = (Server.MapPath("/assets/download/") + $"{guid}.zip");
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


        //数据转换
        public void DataChange(string filePath, EquipmentTypeVO data)
        {
            // 使用 StreamWriter 写入数据到文件
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"主键:{data.id}");
                writer.WriteLine($"设备名称:{data.name}");
                writer.WriteLine($"位置:{data.location}");
                if (data.status == 0)
                    writer.WriteLine($"运行状态:未启用");
                else writer.WriteLine($"运行状态:启用");
                writer.WriteLine($"功率:{data.power}");
                writer.WriteLine($"投入使用时间:{data.usedtime}");
                writer.WriteLine($"设备类型:{data.typename}");
            }

        }

        //启停用
        public JsonResult used(int id, string use)
        {
            return Json(_equipmentService.GetUsedModel(id, use), JsonRequestBehavior.AllowGet);
        }

        //删除
        public JsonResult delete(int id)
        {
            return Json(_equipmentService.Delete(id), JsonRequestBehavior.AllowGet);
        }

        //添加
        public JsonResult add(Equipment equipment)
        {
            return Json(_equipmentService.Add(equipment), JsonRequestBehavior.AllowGet);
        }

        //表单赋值
        public JsonResult getequip(int id)
        {
            return Json(_equipmentService.GetModel(id), JsonRequestBehavior.AllowGet);
        }

        //编辑
        public JsonResult edit(Equipment equipment)
        {
            return Json(_equipmentService.Update(equipment), JsonRequestBehavior.AllowGet);
        }

        //树形组件绑定
        public JsonResult getmenu()
        {
            //传递的vo对象实例
            List<EquipmentTypeVO> list = new List<EquipmentTypeVO>();
            //数据库查询对象
            List<EquipType> mlist = _equipmentService.getTypeList();
            foreach (var item in mlist)
            {
                EquipmentTypeVO mvo = new EquipmentTypeVO();
                mvo.id = item.id;
                mvo.title = item.type_name;
                list.Add(mvo);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}