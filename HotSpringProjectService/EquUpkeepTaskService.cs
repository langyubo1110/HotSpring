using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository;
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
        private readonly IEmployMessageRepository _EmployMessagerepository;

        public EquUpkeepTaskService(IEquUpkeepTaskRepository upkeepTaskRepository, IEmployMessageRepository EmployMessagerepository)
        {
            _upkeepTaskRepository = upkeepTaskRepository;
            _EmployMessagerepository = EmployMessagerepository;
        }

        public ResMessage GetList(EquUpkeepTaskFilter filter)
        {
            IEnumerable<EquUpkeepTaskVO> list = _upkeepTaskRepository.QueryBySql<EquUpkeepTaskVO>($@"select s.*,p.task_name,p.start_time,p.end_time,p.interval,p.task_info,e.name,e.location,e.power,o.name as ename
from Equ_Upkeep_Task s
inner join Equ_Equipment e on e.id=s.equ_id
inner join Equ_Upkeep_Plan p on p.id=s.equ_plan_id
inner join Employ_Emp o on o.id=s.exec_id");

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
            int flag = _upkeepTaskRepository.execBySql($"insert into Equ_Upkeep_Task(equ_plan_id,upkeep_time,QRimg,equ_id,exec_id)values({id},'{time}','{img}',{equ_id},49)");
            return flag;
        }

        public ResMessage upkeepdeit(List<EmployCheckInVO> data, int[] equid, int[] equplanid)
        {
            List<EmployMessage> list = new List<EmployMessage>();
            foreach (var item in equid)
            {
                foreach(var i in equplanid)
                {
                    _upkeepTaskRepository.execBySql($"update Equ_Upkeep_Task set exec_id={data[0].emp_Id} where equ_id={item} and equ_plan_id={i}");
                }
            }
            foreach (var item in equid)
            {
                EmployMessage message = new EmployMessage
                {
                    sender_id = 49,
                    link = "/employtasks/upkeeptasks",
                    recipients_id= data[0].emp_Id,
                    send_time=DateTime.Now,
                    create_time=DateTime.Now,
                    part="您有新的保养任务请前往查看",

                };
                list.Add(message);
            }
            _EmployMessagerepository.AddRange(list);
            return ResMessage.Success("分发成功");
        }
    }
}
