using DotNet.Utilities;
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService.Interface
{
    public interface IRegEquipResService
    {
        ResMessage GetModel(int id);
        //查分页

        ResMessage GetList();
        ResMessage Add(RegEquipRes regEquipRes);
        ResMessage AddWithEquip(int resId);
        ResMessage Delete(int id);
        ResMessage Update(RegEquipRes regEquipRes);
        ResMessage GetListById(int id, int userId);
        
        ResMessage GetListForBind(int id);
    }
}
