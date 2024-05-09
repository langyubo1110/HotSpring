using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEquUpkeepPlanService
    {
        List<EquUpkeepPlan> GetList();
        ResMessage GetListUnion(EquUpkeepPlanFilter filter);
        ResMessage Add(EquUpkeepPlan equUpkeepPlan);
        ResMessage Delete(int id);
        ResMessage Update(EquUpkeepPlan equUpkeepPlan);
        //查实体
        ResMessage GetModel(int id);
    }
}
