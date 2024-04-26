using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Utilities;
using dbs.Utils;
using System.Web.Mvc;

namespace HotSpringProjectService.Interface
{
    public interface IEquipmentService
    {
        //查实体
        ResMessage GetModel(int id);
        //查分页
        ResMessage GetListByPager(int page,int limit);

        ResMessage Add(Equipment equip);
        ResMessage Delete(int id);
        ResMessage Update(Equipment equip);
    }
}
