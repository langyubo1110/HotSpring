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
    public interface IEquUpkeepTaskService
    {
        ResMessage GetList(EquUpkeepTaskFilter filter);
        List<EquUpkeepTaskVO> getlistnofilter();
        List<EquUpkeepTask> GetTaskList();
        int insert(int id, DateTime time, string img,int equ_id);

        //保养任务更新
        ResMessage upkeepdeit(List<EmployCheckInVO> data, int[] equid, int[] equplanid);

    }
}
