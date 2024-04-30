using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Helpers;
using System.Web.Routing;
using System.Net;
using System.IO;

namespace HotSpringProjectService
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipTypeRepository _equipTypeRepository;
        private readonly IRegEquipResRepositpry _regEquipResRepositpry;

        //构造函数注入
        public EquipmentService(IEquipmentRepository equipmentRepository,IEquipTypeRepository equipTypeRepository, IRegEquipResRepositpry regEquipResRepositpry) 
        {
            _equipmentRepository= equipmentRepository;
            _equipTypeRepository = equipTypeRepository;
            _regEquipResRepositpry= regEquipResRepositpry;
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
        //查类型表
        public List<EquipType> getTypeList()
        {
            //查类型表
            List<EquipType> list = _equipTypeRepository.GetList();
            //查状态分组
            //List<Equipment> list = _equipmentRepository.QueryBySql<Equipment>("select e.status  from Equ_Equipment e group by  e.status").ToList();
            return list;
        }
        //查设备表状态分组
        public List<Equipment> getTypestatus()
        {
            //查状态分组
            List<Equipment> list = _equipmentRepository.QueryBySql<Equipment>("select e.status  from Equ_Equipment e group by  e.status").ToList();
            return list;
        }
        
        //查实体
        public ResMessage GetModel(int id)
        {
            Equipment equipment = _equipmentRepository.GetModel(id);
            if (equipment != null)
                return ResMessage.Success(equipment);
            else return ResMessage.Fail();
        }
        //查停用实体
        public ResMessage GetUsedModel(int id, string use)
        {
            Equipment equipment = _equipmentRepository.GetModel(id);
            if (equipment != null)
            {
                if (use == "use")
                {
                    equipment.status = 1;
                    _equipmentRepository.Update(equipment);
                    return ResMessage.Success(equipment);
                }
                else
                {
                    equipment.status = 0;
                    _equipmentRepository.Update(equipment);
                    return ResMessage.Success(equipment);
                }
            }
            else return ResMessage.Fail();
        }
        //设备表关联设备类型表
        public IEnumerable<EquipmentTypeVO> GetListUnion()
        {
            IEnumerable<EquipmentTypeVO> list = _equipmentRepository.QueryBySql<EquipmentTypeVO>($@"select e.*,
            t.type_name as typename , r.create_time as usedtime from Equ_Equipment as e inner join Equ_Type as t on e.equ_type=t.id 
            inner join Reg_Equip_Research as r on e.used_time=r.id");
            return list;
        }
      
        //分页
        public ResMessage GetListByPager(EquipmentFilter filter)
        {
            IEnumerable<EquipmentTypeVO> list = GetListUnion();
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
        public List<EquipmentTypeVO> MakeQuery(IEnumerable<EquipmentTypeVO> list, EquipmentFilter filter, out int count)
        {
            if (!string.IsNullOrEmpty(filter.name))
            {
                list = list.Where(x => x.name.Contains(filter.name)).ToList();
            }
            //类型筛选
            if (filter.type != 0)
            {
                list = list.Where(x => x.equ_type == filter.type);
            }
            //时间筛选
            if (filter.begintime!=null&&filter.endtime!=null)
            {
                list = list.Where(x => x.usedtime>=filter.begintime&&x.usedtime<((DateTime)filter.endtime).AddDays(1));
            }
            //树形筛选
            if (filter.tid != 0)
            {
                list = list.Where(x => x.equ_type == (filter.tid));
            }
            count = list.Count();
            //开启分页
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderByDescending(x => x.id).Skip((filter.page - 1) * filter.limit).Take(filter.limit).ToList();
            }
            List<EquipmentTypeVO> list1=list.ToList();
            return list1;
        }


        //更新
        public ResMessage Update(Equipment equipment)
        {
            int flag = _equipmentRepository.Update(equipment);
            return flag>0 ? ResMessage.Success() : ResMessage.Fail();
        }
    }
}
