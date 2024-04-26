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
        IEnumerable<EmployEmp> GetList();

        ResMessage GetListByPager(EmployEmpFilter filter);

        ResMessage Add(EmployEmp movies);
        ResMessage Delete(int Id);
        ResMessage Update(EmployEmp movies);
        ResMessage getModel(int id);
    }
}
