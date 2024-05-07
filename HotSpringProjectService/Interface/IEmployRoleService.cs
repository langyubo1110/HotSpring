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
        IEnumerable<EmployRole> GetEmployRoles();
        ResMessage GetRoles();
        ResMessage Add(EmployRole employRole);
    }
}
