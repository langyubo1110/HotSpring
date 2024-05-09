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
    public interface IEquToTaskService
    {
        ResMessage GetList(int id,EquUpkeepPlanFilter filter);
        ResMessage AddAfterDelete(List<EquToTaskVO> getData, EquUpkeepPlanFilter filter ,int id);
        //ResMessage Delete();
        List<EquToTaskVO> GetListAll();
    }
}
