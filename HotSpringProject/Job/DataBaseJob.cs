using HotSpringProjectService.Interface;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HotSpringProject.Job
{
    public class DataBaseJob:IJob
    {
        private readonly IBackUpDataBaseService _backUpDataBase;
        //任务必须要有无参构造函数，此时注入了 没有无参构造，无法进行调度

        public DataBaseJob(IBackUpDataBaseService backUpDataBase)
        {
            _backUpDataBase = backUpDataBase;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _backUpDataBase.Execute();
        }
    }
}