
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


/*  郎于博
 *  2024-04-25
 *  设备控制器
 */
namespace HotSpringProject.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;



        //多个表注入
        //private readonly IMovieTypeService _movieTypeService;

        //构造函数注入
        public EquipmentController (IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        #region 页面
        public ActionResult Index()
        {
            ViewBag.EquipType = _equipmentService.getTypeList();
            return View();
        }
        public ActionResult detail(int id=0)
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
        #endregion


        #region 接口
        public JsonResult Query(EquipmentFilter filter)
        {
            return Json(_equipmentService.GetListByPager(filter), JsonRequestBehavior.AllowGet);
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
                return Json(ResMessage.Success(data:"/assets/upload/" + file.FileName));
            }
            catch (Exception ex)
            {
                return Json(ResMessage.Fail(ex.Message));
            }
        }
        //文件下载接口
        public JsonResult downloading(EquipmentTypeVO data)
        {
                // 文件路径
                string filePath = (Server.MapPath("/assets/download/") + $" {data.id}.txt");
                // 写入数据到文件
                try
                {
                DataChange(filePath, data);
                return Json(ResMessage.Success("数据已成功保存到文件中。"));
                }
                catch (Exception ex)
                {
                return Json(ResMessage.Fail($"{ex.Message}"));
                }
        }

        //文件多选下载接口
        public JsonResult downselect(List<EquipmentTypeVO> data)
        {
            List<string> filesToCompress = new List<string>();
            //获取当前guid避免id重复
            string guid = Guid.NewGuid().ToString();
            string zipPath = (Server.MapPath("/assets/download/") + $"{guid}.zip");
           
            //循环遍历勾选的行数据
            foreach (var item in data)
            {
                // 文件路径
                string filePath = (Server.MapPath($"/assets/download/") + $"{item.id}.txt");
                // 写入数据到文件
                DataChange(filePath, item);
                filesToCompress.Add(filePath);
            }
            // 创建一个新的ZIP文件
            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (string file in filesToCompress)
                {
                    // 将每个文件添加到ZIP存档中
                    archive.CreateEntryFromFile(file, Path.GetFileName(file));
                }
            }
            return Json(ResMessage.Success("勾选行数据已保存在压缩包下"));
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
                writer.WriteLine($"创建时间:{data.create_time}");
            }
        }
       
        //启停用
        public JsonResult used(int id ,string use)
        {
            return Json(_equipmentService.GetUsedModel(id,use), JsonRequestBehavior.AllowGet);
        }

        //删除
        public JsonResult delete(int id)
        {
            return Json(_equipmentService.Delete(id),JsonRequestBehavior.AllowGet);
        }

        //添加
        public JsonResult add(Equipment equipment)
        {
            return Json(_equipmentService.Add(equipment), JsonRequestBehavior.AllowGet);
        }

        //表单赋值
        public JsonResult getequip(int id)
        {
            return Json(_equipmentService.GetModel(id),JsonRequestBehavior.AllowGet);
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