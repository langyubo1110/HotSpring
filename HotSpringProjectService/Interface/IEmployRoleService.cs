using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IEmployRoleService
    {
        ResMessage GetModel(int id);
        //查分页
        IEnumerable<EmployRole> GetList();

        ResMessage Add(EmployRole EmployRole);
        ResMessage Delete(int id);
        ResMessage Update(EmployRole EmployRole);
    }
}
