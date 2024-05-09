using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface ISystemPagesService
    {
        IEnumerable<SystemPages> GetList();

        ResMessage GetListByPager(PagesFilter filter);

        ResMessage Add(SystemPages systemPages);
        ResMessage Delete(int Id);
        ResMessage Update(SystemPages systemPages);
        ResMessage getModel(int id);
        IEnumerable<(int Id, string Name)> GetModuleList();

        //IEnumerable<SystemPages> GetRoleList(int roleId);
        List<SystemPages> GetPagesByModuleId(int moduleId);
    }
}
