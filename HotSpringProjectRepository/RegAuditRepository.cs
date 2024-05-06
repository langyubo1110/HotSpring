using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RegAuditRepository : IRegAuditRepository
    {
        private readonly HotSpringDbContext _db;

        public RegAuditRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(RegAudit regAudit)
        {
            regAudit.create_time = DateTime.Now;
            _db.Entry(regAudit).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public bool Delete(int id)
        {
            RegAudit regAudit = _db.regAudit.Find(id);
            _db.Entry(regAudit).State = EntityState.Deleted;
            int flag = _db.SaveChanges();
            if (flag > 0)
                return true;
            else
                return false;

        }

        public IEnumerable<RegAudit> GetList()
        {
           
            return _db.regAudit.ToList();
        }
        
        

        public RegAudit GetModel(int id)
        {
            RegAudit regAudit = _db.regAudit.Find(id);
            return regAudit;
        }

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }

        public bool Update(RegAudit regAudit)
        {
            //当调用此方法设置审核意见为1通过
            //regAudit.recheck_opin = 1;
            _db.Entry(regAudit).State = System.Data.Entity.EntityState.Modified;
            int flag = _db.SaveChanges();
            if (flag > 0)
                return true;
            else
                return false;
        }
    }
}
