using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HotSpringProjectService.Interface
{
    public interface ISystemPageCorrespondenceService
    {
        List<SystemPageCorrespondence> GetList();


        ResMessage Add(int roleId, List<int> pageIds);
        ResMessage Delete(int roleId);
        ResMessage Update(SystemPageCorrespondence movies);

        ResMessage GetModel(int Id);

        List<MenuVO> GetMenu(int RoleId);
        List<MenuVO> GetAllPages(int role_id);
        bool verify(int roleId, int pageId);
    }
}
