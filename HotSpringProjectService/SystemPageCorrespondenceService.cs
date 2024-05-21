using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class SystemPageCorrespondenceService : ISystemPageCorrespondenceService
    {
        private readonly ISystemPageCorrespondenceRepository _rolePageRepository;
        private readonly IEmployRoleRepository _dbRoled;

        public SystemPageCorrespondenceService(ISystemPageCorrespondenceRepository rolePageRepository,IEmployRoleRepository employRoleRepository)
        {
            _rolePageRepository = rolePageRepository;
            _dbRoled = employRoleRepository;
        }
        public ResMessage Add(int roleId, List<int> pageIds)
        {
            int flag = 0;
            flag = _rolePageRepository.Add(roleId, pageIds);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public ResMessage Delete(int roleId)
        {
            bool flag = _rolePageRepository.Delete(roleId);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        public List<SystemPageCorrespondence> GetList()
        {
            throw new NotImplementedException();
        }
        public List<MenuVO> GetAllPages(int role_id)
        {
            int roleId = role_id;
            List<PagesVO> list = _rolePageRepository.QueryBySql<PagesVO>($@"select p.*,m.module_name from System_Pages p 
                                                                         inner join System_Module m on m.id=p.module_id");
            List<MenuVO> resulitList = new List<MenuVO>();
            //查出对应关系表的所有数据
            foreach (var gp in list.GroupBy(x => x.module_name))
            {
                MenuVO menu = new MenuVO();
                menu.module_name = gp.Key;
                List<SystemPages> plist = new List<SystemPages>();
                foreach (var p in gp.ToList())
                {
                    SystemPages page = new SystemPages();
                    page.page_name = p.page_name;
                    page.id = p.id;
                    page.page_address = p.page_address;
                    bool exists = verify(roleId,p.id);
                    page.isChecked = exists;
                    plist.Add(page);
                }
                menu.page_list = plist;
                resulitList.Add(menu);
            }
            return resulitList;
        }
        public List<MenuVO> GetMenu(int RoleId)
        {
            List<SystemPageCorrespondenceDTO> list = _rolePageRepository.QueryBySql<SystemPageCorrespondenceDTO>($@"select rp.*,p.page_name,p.page_address,m.module_name,m.icon from System_Page_Correspondence rp 
                                                                        inner join System_Pages p on p.id=rp.pages_id
                                                                        inner join System_Module m on m.id=p.module_id  where rp.role_id={RoleId}");
            List<MenuVO> resulitList = new List<MenuVO>();
            //查出对应关系表的所有数据
            foreach (var gp in list.GroupBy(x => x.module_name))
            {
                MenuVO menu = new MenuVO();
                menu.module_name = gp.Key;
                var icon = _rolePageRepository.QueryBySql<string>($@"select m.icon from System_Page_Correspondence rp 
                                                        inner join System_Pages p on p.id=rp.pages_id
                                                        inner join System_Module m on m.id=p.module_id
                                                        where rp.role_id={RoleId} and m.module_name='{gp.Key}'").FirstOrDefault();
                if (!string.IsNullOrEmpty(icon))
                {
                    icon = icon.Replace("\"", ""); // 移除双引号
                }
                menu.icon = icon;
                List<SystemPages> plist = new List<SystemPages>();
                foreach (var p in gp.ToList())
                {
                    SystemPages page = new SystemPages();

                    page.page_name = p.page_name;
                    page.page_address = p.page_address;
                    plist.Add(page);
                }
                menu.page_list = plist;
                resulitList.Add(menu);
            }

            return resulitList;

        }

        public ResMessage GetModel(int Id)
        {
            throw new NotImplementedException();
        }

        public ResMessage Update(SystemPageCorrespondence movies)
        {
            throw new NotImplementedException();
        }

        public bool verify(int roleId,int pageId)
        {
           var page = _rolePageRepository.GetList().Where(rp => rp.role_id == roleId).Select(rp => rp.pages_id);
            if (page.Contains(pageId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       

    }
}
