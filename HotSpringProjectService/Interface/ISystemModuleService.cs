using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface ISystemModuleService
    {
        IEnumerable<SystemModule> GetList();
        //IEnumerable<ModuleVO> GetMenuList();
    }
}
