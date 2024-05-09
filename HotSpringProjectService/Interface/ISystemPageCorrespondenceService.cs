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
        List<SystemPageCorrespondenceService> GetList();


        ResMessage Add(SystemPageCorrespondenceService movies);
        ResMessage Delete(int Id);
        ResMessage Update(SystemPageCorrespondenceService movies);

        ResMessage GetModel(int Id);

        List<MenuVO> GetMenu(int RoleId);
        bool verify(int roleId, int pageId);
    }
}
