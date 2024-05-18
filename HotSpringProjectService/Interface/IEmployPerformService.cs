using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEmployPerformService
    {
        ResMessage Delete(int id);
        ResMessage Update(EmployPerform employPerform);
        ResMessage Add(EmployPerform employPerform);
        ResMessage GetModel(int id);
        ResMessage GetList();
        ResMessage GetListByPager(int page, int limit,string yyyy_MM, int emp_id);
        List<EmployPerform> GetListByRepairId(int id);
    }
}
