using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotSpringProjectService.Interface
{
    public interface ISystemPageCorrespondenceService
    {
        List<SystemPageCorrespondence> GetList();


        ResMessage Add(int roleId, List<int> pageIds);
        ResMessage Delete(int Id);
        ResMessage Update(SystemPageCorrespondence movies);

        ResMessage GetModel(int Id);

        List<MenuVO> GetMenu(int RoleId);
        bool verify(int roleId, int pageId);
        List<MenuVO> GetAllPages(int role_id);
    }
}
