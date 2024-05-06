using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface ISystemModuleRepository
    {
        IEnumerable<SystemModule> GetList();

    }
}
