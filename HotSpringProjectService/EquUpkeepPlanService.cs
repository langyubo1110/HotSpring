using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class EquUpkeepPlanService : IEquUpkeepPlanService
    {
        private readonly IEquUpkeepPlanRepository _upkeepPlanRepository;

        public EquUpkeepPlanService(IEquUpkeepPlanRepository upkeepPlanRepository)
        {
            _upkeepPlanRepository = upkeepPlanRepository;
        }

        public List<EquUpkeepPlan> GetList()
        {
            IEnumerable<EquUpkeepPlan> list = _upkeepPlanRepository.GetList();
            return list.ToList();
        }

        /// <summary>
        /// 条件筛查
        /// </summary>
        /// <param name="list">全表数据</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public List<EquPlanVO> MakeQuery(IEnumerable<EquPlanVO> list, EquUpkeepPlanFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.name))
            {
                list = list.Where(x => x.equ_name.Contains(filter.name)).ToList();
            }
            //开启分页
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderBy(x => x.id).Skip((filter.page - 1) * filter.limit).Take(filter.limit).ToList();
            }
            List<EquPlanVO> list1 = list.ToList();
            return list1;
        }

        ResMessage IEquUpkeepPlanService.Add(EquUpkeepPlan equUpkeepPlan)
        {
            int flag = _upkeepPlanRepository.Add(equUpkeepPlan);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        ResMessage IEquUpkeepPlanService.Delete(int id)
        {
            bool flag = _upkeepPlanRepository.Delete(id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        ResMessage IEquUpkeepPlanService.GetListUnion(EquUpkeepPlanFilter filter)
        {
            IEnumerable<EquPlanVO> list = _upkeepPlanRepository.QueryBySql<EquPlanVO>($@"select t.task_name,s.*  from Equ_Upkeep_Task s
inner join Equ_Upkeep_Plan t on s.equ_plan_id=t.id
 ");
            List<EquPlanVO> list1 = list.ToList();
            list = MakeQuery(list, filter);
            return ResMessage.Success(list, list1.Count);
        }

        ResMessage IEquUpkeepPlanService.GetModel(int id)
        {
            EquUpkeepPlan equUpkeepPlan = _upkeepPlanRepository.GetModel(id);
            if (equUpkeepPlan != null)
                return ResMessage.Success(equUpkeepPlan);
            else return ResMessage.Fail();
        }

        ResMessage IEquUpkeepPlanService.Update(EquUpkeepPlan equUpkeepPlan)
        {
            int flag = _upkeepPlanRepository.Update(equUpkeepPlan);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
