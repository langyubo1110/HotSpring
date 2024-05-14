using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            IEnumerable<EquUpkeepTaskVO> list = _upkeepTaskRepository.QueryBySql<EquUpkeepTaskVO>($@"select t.*,s.equ_plan_id,s.upkeep_time,s.distribute_time,s.feedback_time,s.distribute_id,s.exec_id
,s.upkeep_feedback_info,s.QRimg,q.equ_id,q.task_id,i.name,i.location,y.type_name,i.power
from Equ_Upkeep_Task s inner join Equ_Upkeep_Plan t on s.equ_plan_id=t.id
inner join Equ_To_Taskes q on q.task_id=s.id
inner join Equ_Equipment i on i.id=q.equ_id
inner join Equ_Type y on y.id=i.equ_type");

            List<EquUpkeepTaskVO> list1 = list.ToList();
            int count = list1.Count;
            list = MakeQuery(list, filter);
            return ResMessage.Success(list, count);
        }

        public List<EquUpkeepTaskVO> getlistnofilter()
        {
            IEnumerable<EquUpkeepTaskVO> list = _upkeepTaskRepository.QueryBySql<EquUpkeepTaskVO>($@"select * from Equ_Upkeep_Plan ");
            return list.ToList();
        }
        public List<EquUpkeepTask> GetTaskList()
        {
            IEnumerable<EquUpkeepTask> list = _upkeepTaskRepository.QueryBySql<EquUpkeepTask>($@"select * from Equ_Upkeep_Task ");
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

        public int insert(int id, DateTime time, string img, int equ_id)
        {
            int flag = _upkeepTaskRepository.execBySql($"insert into Equ_Upkeep_Task(equ_plan_id,upkeep_time,QRimg,equ_id)values({id},'{time}','{img}',{equ_id})");
            return flag;
        }

        ResMessage IEquUpkeepTaskService.upkeepdeit(List<EmployCheckInVO> data, int[] equid, int[] equplanid)
        {
            foreach (var item in equid)
            {
                foreach(var i in equplanid)
                {
                    _upkeepTaskRepository.execBySql($"update Equ_Upkeep_Task set exec_id={data[0].emp_Id} where equ_id={item} and equ_plan_id={i}");
                }
            }
            return ResMessage.Success("分发成功");
        }
    }
}
