using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using HotSpringProjectService.Interface;
using HotSpringProjectService;

namespace HotSpringProject.Job
{
    //任务：业务在业务层 
    //需要把WriteTimeJobService注入到当前任务中
    public class MyJob : IJob
    {
        private readonly IWriteDataJobService _writeDataJobService;

        //任务必须要有无参构造函数，此时注入了 没有无参构造，无法进行调度

        public MyJob(IWriteDataJobService writeDataJobService)
        {
            _writeDataJobService = writeDataJobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _writeDataJobService.Execute();
        }
    }
}