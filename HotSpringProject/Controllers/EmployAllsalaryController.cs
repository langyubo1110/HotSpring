using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectService;
using HotSpringProjectService.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Controllers
{
    public class EmployAllsalaryController : Controller
    {
        private readonly IEmployAllsalaryService _employAllsalaryService;

        public EmployAllsalaryController(IEmployAllsalaryService employAllsalaryService) 
        {
            _employAllsalaryService=employAllsalaryService;
        }
        #region 页面
        public ActionResult AllSalary()
        {
            return View();
        }
        #endregion

        #region 接口
        public JsonResult Delete(int id)
        {
            ResMessage resMessage = _employAllsalaryService.Delete(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList()
        {
            ResMessage resMessage = _employAllsalaryService.GetList();
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListByPager(EmployAllsalaryFilter filter)
        {
            ResMessage resMessage = _employAllsalaryService.GetListByPager(filter);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModel(int id)
        {
            ResMessage resMessage = _employAllsalaryService.GetModel(id);
            return Json(resMessage, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(EmployAllsalary employAllsalary)
        {
            ResMessage resMessage = _employAllsalaryService.Add(employAllsalary);
            return Json(resMessage);
        }
        public JsonResult Update(EmployAllsalary employAllsalary)
        {
            ResMessage resMessage = _employAllsalaryService.Update(employAllsalary);
            return Json(resMessage);
        }

        //报表导出接口
        public ActionResult excel()
        {
            //获取上个月份
            DateTime time=DateTime.Now.AddMonths(-1);
            string yyyy_MM=time.ToString("yyyy-MM");
            List<EmployAllsalaryVO> list= _employAllsalaryService.GetExcel(yyyy_MM);
            // 创建Excel文件
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("薪资");
                // 合并单元格
                worksheet.Cells["A1:AK1"].Merge = true;
                // 添加表头
                worksheet.Cells["A1"].Value = "工资表";
                worksheet.Cells["e3"].Value = "所属月份";
                worksheet.Cells["e4"].Value = $"{yyyy_MM}";
                worksheet.Cells["e5"].Value = "发放日期";
                worksheet.Cells["e6"].Value = $"{list[0].pay_time}";

                // 填充薪资数据
                //int rowIndex = 2;
                //worksheet.Cells[string.Format("A{0}", rowIndex)].Value = data.EmployeeName;
                //worksheet.Cells[string.Format("B{0}", rowIndex)].Value = data.SalaryAmount;

                // 将Excel文件转换为字节数组
                byte[] excelBytes = package.GetAsByteArray();

                // 返回Excel文件
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Salary.xlsx");
            }
        }
        #endregion
    }
}