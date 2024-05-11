using DotNet.Utilities;
using HotSpringProject.Entity;
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
    public class EquUpkeepTaskService : IEquUpkeepTaskService
    {
        private readonly IEquUpkeepTaskRepository _upkeepTaskRepository;

        public EquUpkeepTaskService(IEquUpkeepTaskRepository upkeepTaskRepository)
        {
            _upkeepTaskRepository = upkeepTaskRepository;
        }

        public ResMessage GetList(EquUpkeepTaskFilter filter)
        {
            IEnumerable<EquUpkeepTaskVO> list = _upkeepTaskRepository.QueryBySql<EquUpkeepTaskVO>($@"select t.task_name,s.*,t.start_time,t.end_time,t.interval,t.task_info
                                                from Equ_Upkeep_Task s inner join Equ_Upkeep_Plan t on s.equ_plan_id=t.id");

            List<EquUpkeepTaskVO> list1 = list.ToList();
            int count=list1.Count;
            list = MakeQuery(list, filter);
            return ResMessage.Success(list,count);
        }

        public List<EquUpkeepTaskVO> getlistnofilter()
        {
                IEnumerable<EquUpkeepTaskVO> list = _upkeepTaskRepository.QueryBySql<EquUpkeepTaskVO>($@"select t.task_name,s.*,t.start_time,t.end_time,t.interval,t.task_info
                                                    from Equ_Upkeep_Task s inner join Equ_Upkeep_Plan t on s.equ_plan_id=t.id");
            return list.ToList();
        }
        /// <summary>
        /// 条件筛查
        /// </summary>
        /// <param name="list">全表数据</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public List<EquUpkeepTaskVO> MakeQuery(IEnumerable<EquUpkeepTaskVO> list, EquUpkeepTaskFilter filter)
        {
            //开启分页
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderBy(x => x.id).Skip((filter.page - 1) * filter.limit).Take(filter.limit).ToList();
            }
            List<EquUpkeepTaskVO> list1 = list.ToList();
            return list1;
        }

        public int insert(int id,DateTime time,string img)
        {
            int flag= _upkeepTaskRepository.execBySql($"insert into Equ_Upkeep_Task(equ_plan_id,upkeep_time,QRimg)values({id},'{time}','{img}')");
            return flag;
        }

    }
}
