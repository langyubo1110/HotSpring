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
using System.Web.UI.WebControls;

namespace HotSpringProjectService
{
    public class SystemModuleService:ISystemModuleService
    {
        private readonly ISystemModuleRepository _db;
        private readonly ISystemPagesRepository _db1;

        public SystemModuleService(ISystemModuleRepository systemModuleRepository,ISystemPagesRepository systemPagesRepository)
        {
            _db = systemModuleRepository;
            _db1 = systemPagesRepository;
        }

        public IEnumerable<SystemModule> GetList()
        {
            return _db.GetList();
        }

        //public IEnumerable<ModuleVO> GetMenuList()
        //{
        //    var menuList = new List<ModuleVO>();
        //    var menuItems = _db.GetList();

        //    foreach (var menuItem in menuItems)
        //    {
        //        var vo = new ModuleVO();
        //        {
        //            vo.id = menuItem.id;
        //            vo.module_name = menuItem.module_name;
        //            vo.create_time = menuItem.create_time;
        //            vo.icon = menuItem.icon;
        //            vo.systemPages = _db1.GetList().Where(p => p.module_id == menuItem.id);
        //        };
        //        menuList.Add(vo);
        //    }

        //    return menuList;
        //}

    }
}
