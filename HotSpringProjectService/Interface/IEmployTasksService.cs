using DotNet.Utilities;
using HotSpringProject.Entity.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEmployTasksService
    {
        //IEnumerable<EquUpkeepTaskVO> GetList(int reportID);
        ResMessage UpdateTask(int id, string data);
    }
}
