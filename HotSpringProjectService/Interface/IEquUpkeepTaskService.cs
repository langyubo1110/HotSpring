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
        int insert(int id, DateTime time, string img);
    }
}
