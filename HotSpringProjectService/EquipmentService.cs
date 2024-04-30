
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
        public ResMessage GetListByPager(EquipmentFilter filter)
        {
            IEnumerable<Equipment> list = _equipmentRepository.GetListByPager();
            int count = 0;
            list = MakeQuery(list, filter, out count);
            return ResMessage.Success(list, count);
        }
        /// <summary>
        /// 条件筛查
        /// </summary>
        /// <param name="list">全表数据</param>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        public List<Equipment> MakeQuery(IEnumerable<Equipment> list, EquipmentFilter filter, out int count)
        {
            if (!string.IsNullOrEmpty(filter.name))
            {
                list = list.Where(x => x.name.Contains(filter.name));
            }
            //类型筛选
            //if (filter.type != 0 && filter.type != null)
            //{
            //    list = list.Where(x => x.type == filter.type);
            //}
            count = list.Count();
            //开启分页
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderByDescending(x => x.id).Skip((filter.page - 1) * filter.limit).Take(filter.limit);
            }
            return list.ToList();
        }

        //更新
        public ResMessage Update(Equipment equipment)
        {
            int id=equipment.id;
            List<Equipment> list = _equipmentRepository.QueryBySql<Equipment>($"select * from Equ_Equipment where id={id}").ToList();
            equipment.create_time = list[0].create_time;
            equipment.used_time = list[0].used_time;
            int flag = _equipmentRepository.Update(equipment);
            return flag>0 ? ResMessage.Success() : ResMessage.Fail();
        }

       
    }
}
