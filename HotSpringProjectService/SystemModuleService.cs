using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class SystemModuleService:ISystemModuleService
    {
        private readonly ISystemModuleRepository _db;

        public SystemModuleService(ISystemModuleRepository systemModuleRepository)
        {
            _db = systemModuleRepository;
        }

        public List<SystemModule> GetList()
        {
            return _db.GetList();
        }

    }
}
