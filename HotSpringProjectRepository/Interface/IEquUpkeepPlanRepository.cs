using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEquUpkeepPlanRepository
    {
        IEnumerable<EquUpkeepPlan> GetList();
        //sql查询
        IEnumerable<T> QueryBySql<T>(string sql);
        //增
        int Add(EquUpkeepPlan equUpkeepPlan);
        //改
        int Update(EquUpkeepPlan equUpkeepPlan);
        //删
        bool Delete(int id);
        //查实体
        EquUpkeepPlan GetModel(int id);
    }
}
