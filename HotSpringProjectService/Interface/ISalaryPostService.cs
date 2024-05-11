using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    //薪资发放定时调度
    //2024-05-10
    //裴晨旭
    public interface ISalaryPostService
    {
        Task Execute();
    }
}
