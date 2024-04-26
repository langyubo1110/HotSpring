using dbs.Utils;
using HotSpringProject.Entity;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HotSpringProjectService
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        //构造函数注入
        public EquipmentService(IEquipmentRepository equipmentRepository) 
        {
            _equipmentRepository= equipmentRepository;
        }
        //增
        public ResMessage Add(Equipment equip)
        {
            int flag = _equipmentRepository.Add(equip);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        //删
        public ResMessage Delete(int id)
        {
            bool flag = _equipmentRepository.Delete(id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }
        //查实体
        public ResMessage GetModel(int id)
        {
           Equipment equipment=_equipmentRepository.GetModel(id);
           if (equipment != null)
                return ResMessage.Success(equipment);
           else return ResMessage.Fail();
        }
        //分页
        public ResMessage GetListByPager(int page,int limit)
        {
            List<Equipment> list1 = _equipmentRepository.GetListByPager();
            List<Equipment> list2 = list1.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            //{data code msg count}
            return ResMessage.Success(list2, list1.Count);
            //对仓储层进行数据业务加工  分页展示
            //获得全表数据
        }
        //更新
        public ResMessage Update(Equipment equipment)
        {
            int flag = _equipmentRepository.Update(equipment);
            return flag>0 ? ResMessage.Success() : ResMessage.Fail();
        }

       
    }
}
