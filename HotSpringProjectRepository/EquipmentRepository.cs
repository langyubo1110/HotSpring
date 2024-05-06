using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly HotSpringDbContext _db;

        //构造函数注入
        public EquipmentRepository(HotSpringDbContext HotSpringDbContext) 
        {
            _db= HotSpringDbContext;
        }

        //增
        public int Add(Equipment equ)
        {
            _db.Entry(equ).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        //删
        public bool Delete(int id)
        {
            Equipment equipment=_db.Equipment.Find(id);
            if (equipment != null)
            {
                _db.Entry(equipment).State = System.Data.Entity.EntityState.Deleted;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        //查设备表全表
        public IEnumerable<Equipment> GetListByPager()
        {
            IEnumerable<Equipment> list = _db.Equipment;
            return list;
        } 

        //查实体
        public Equipment GetModel(int id)
        {
            Equipment equipment = _db.Equipment.Find(id);
            return equipment;
        }
        

        //查sql

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }

        //更新
        public int Update(Equipment equ)
        {
            _db.Entry(equ).State = System.Data.Entity.EntityState.Modified;
            int flag= _db.SaveChanges();
            return flag;
        }
    }
}
