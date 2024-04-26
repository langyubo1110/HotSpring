using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEmployEmpService
    {
        List<EmployEmp> GetList();

        ResMessage GetListByPager(int page, int limit);

        ResMessage Add(EmployEmp movies);
        ResMessage Delete(int Id);
        ResMessage Update(EmployEmp movies);
        ResMessage getModel(int id);
    }
}
