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
    public interface IEmployCheckInService
    {
        ResMessage Add(int empId, int type);
        IEnumerable<EmployCheckIn> GetList();
        IEnumerable<EmployCheckInVO> GetListUnionSql();
        double GetWorkRate(int id);
    }
}
