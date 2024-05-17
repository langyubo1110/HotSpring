using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class EquToTaskService : IEquToTaskService
    {
        private readonly IEquToTaskRepository _equToTaskRepository;
        private readonly IEquUpkeepTaskService _equUpkeepTaskService;

        public EquToTaskService(IEquToTaskRepository equToTaskRepository,IEquUpkeepTaskService equUpkeepTaskService)
        {
            _equToTaskRepository = equToTaskRepository;
            _equUpkeepTaskService= equUpkeepTaskService;
        }

        public ResMessage AddAfterDelete(List<EquToTaskVO> getData, EquUpkeepPlanFilter filter, int id)
        {
            //删除全表
            int flag = _equToTaskRepository.delete(id);

            //添加右边数据
            if (getData != null)
            {
                foreach (var item in getData)
                {
                    //IEnumerable<EquUpkeepTask> tlist= _equUpkeepTaskService.GetTaskList();
                    //List<EquUpkeepTask> tlists=tlist.Where(t=>t.equ_plan_id==item.value).ToList();
                    _equToTaskRepository.execBySql($"insert into Equ_To_Taskes(equ_id,equ_plan_id)values({id},{item.value})");
                }
                IEnumerable<EquToTaskVO> list = _equToTaskRepository.QueryBySql<EquToTaskVO>("select * from Equ_To_Taskes").ToList();
                List<EquToTaskVO> list1 = list.ToList();
                int count = list1.Count;
                list1 = MakeQuery(list1, filter);
                return ResMessage.Success(list1, count);
            }
            return ResMessage.Success();    
        }


        public ResMessage GetList(int id, EquUpkeepPlanFilter filter)
        {
            List<EquToTaskVO> listvo = _equToTaskRepository.QueryBySql<EquToTaskVO>($@"select t.equ_id,t.equ_plan_id,s.task_name,s.start_time,s.end_time,s.interval,s.importance,s.task_info from Equ_To_Taskes t inner join Equ_Upkeep_Plan s
                                                        on t.equ_plan_id=s.id where equ_id={id}").ToList();
            int count = listvo.Count;
            listvo = MakeQuery(listvo, filter);
            return ResMessage.Success(listvo, count);
        }
        
        //查vo全表
        public List<EquToTaskVO> GetListAll()
        {
            List<EquToTaskVO> listvo = _equToTaskRepository.QueryBySql<EquToTaskVO>($@"select t.*,u.start_time,u.end_time,u.interval,u.task_name 
                                       from Equ_To_Taskes t inner join Equ_Upkeep_Plan as u on u.id=t.equ_plan_id ").ToList();
            return listvo;
        }

        public List<EquToTaskVO> MakeQuery(IEnumerable<EquToTaskVO> list, EquUpkeepPlanFilter filter)
        {
            //开启分页
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderBy(x => x.id).Skip((filter.page - 1) * filter.limit).Take(filter.limit).ToList();
            }
            List<EquToTaskVO> list1 = list.ToList();
            return list1;
        }
    }
}
