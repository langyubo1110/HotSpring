using HotSpringProjectService;
using HotSpringProjectService.Interface;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HotSpringProject.Job
{
    //薪资发放定时调度触发器
    public class SalaryPostJob:IJob
    {
        private readonly ISalaryPostService _salaryPostService;

        public SalaryPostJob(ISalaryPostService salaryPostService) 
        {
            _salaryPostService=salaryPostService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _salaryPostService.Execute();
        }
    }
}