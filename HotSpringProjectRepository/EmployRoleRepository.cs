using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EmployRoleRepository:IEmployRoleRepository
    {
        private readonly HotSpringDbContext _db;

        //构造函数注入
        public EmployRoleRepository(HotSpringDbContext HotSpringDbContext)
        {
            _db = HotSpringDbContext;
        }
        //增
        public int Add(EmployRole EmployRole)
        {
            _db.Entry(EmployRole).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        //删
        public bool Delete(int id)
        {
            EmployRole EmployRole = _db.EmployRole.Find(id);
            if (EmployRole != null)
            {
                _db.Entry(EmployRole).State = System.Data.Entity.EntityState.Deleted;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        //查设备表全表
        public IEnumerable<EmployRole> GetList()
        {
            IEnumerable<EmployRole> list = _db.EmployRole;
            return list;
        }

        //查实体
        public EmployRole GetModel(int id)
        {
            EmployRole EmployRole = _db.EmployRole.Find(id);
            return EmployRole;
        }


        //查sql

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }

        //更新
        public bool Update(EmployRole EmployRole)
        {
            _db.Entry(EmployRole).State = System.Data.Entity.EntityState.Modified;
            int flag = _db.SaveChanges();
            return flag > 0 ? true : false;
        }
    }
}
