using HotSpringProject.Entity.VO;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        //入库一条数据
                        _equUpkeepTaskService.insert(item.equ_plan_id, time);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
