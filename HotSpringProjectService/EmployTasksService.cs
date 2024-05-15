using DotNet.Utilities;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class EmployTasksService : IEmployTasksService
    {
        private readonly IEquUpkeepTaskRepository _dbUp;

        public EmployTasksService(IEquUpkeepTaskRepository upkeepTaskRepository)
        {
            _dbUp = upkeepTaskRepository;
        }
        //public IEnumerable<EquUpkeepTaskVO> GetList(int reportID)
        //{
        //    return _dbUp.execBySql($"");
        //}
        public ResMessage UpdateTask(int id, string data)
        {
            _dbUp.execBySql($"update Equ_Upkeep_Task set upkeep_feedback_info='{data}' , status = 1 where equ_plan_id={id}");
            return ResMessage.Success("反馈成功");
        }
    }
}
