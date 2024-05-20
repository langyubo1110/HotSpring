using HotSpringProject.Entity.VO;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using QRCoder;

namespace HotSpringProjectService
{
    public class WriteDataJobService : IWriteDataJobService
    {
        private readonly IEquToTaskService _equToTaskService;
        private readonly IEquUpkeepTaskService _equUpkeepTaskService;

        public WriteDataJobService(IEquToTaskService equToTaskService,IEquUpkeepTaskService equUpkeepTaskService)
        {
            _equToTaskService = equToTaskService;
            _equUpkeepTaskService = equUpkeepTaskService;
        }
        public Task Execute()
        {
            DateTime time= DateTime.Now;
            //EqutotaskVO查全表 
            List<EquToTaskVO> list= _equToTaskService.GetListAll();
            foreach(var item in list)
            {
                //保养未过期
                if (item.end_time >= time&&item.start_time<=time)
                {
                    //获取间隔天数
                    TimeSpan interval = time - item.start_time;
                    int daysDifference = interval.Days;
                    //需要保养
                    if (daysDifference % item.interval == 0)
                    {
                        // 生成包含参数的链接
                        string targetUrl = $"https://localhost:44364/equupkeeptask/upkeeptask?id={item.equ_plan_id}&equ_id={item.equ_id}";
                        // 生成二维码
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(targetUrl, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(10);

                        // 保存二维码图片到文件夹
                        string imagePath = HostingEnvironment.MapPath($"/assets/QRimg/{item.equ_id}_{item.equ_plan_id}.png");
                        string imgurl = $"/assets/QRimg/{item.equ_id}_{item.equ_plan_id}.png";
                        qrCodeImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

                        //入库一条数据
                        _equUpkeepTaskService.insert(item.equ_plan_id, time, imgurl,item.equ_id);
                    }
                }
            }
            return Task.CompletedTask;
        }

       
    }
}
