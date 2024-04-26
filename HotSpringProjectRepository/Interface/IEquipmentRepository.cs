using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository.Interface
{
    public interface IEquipmentRepository
    {
        //查
        List<Equipment> GetListByPager();
        //增
        int Add(Equipment equ);
        //改
        int Update(Equipment equ);
        //删
        bool Delete(int id);
        //查实体
        Equipment GetModel(int id);
    }
}
