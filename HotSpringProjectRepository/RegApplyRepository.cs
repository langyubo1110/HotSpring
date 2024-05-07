
using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RegApplyRepository : IRegApplyRepository
    {
        private readonly HotSpringDbContext _db;
        private DbContextTransaction _dbTransaction;
        public RegApplyRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(RegApply regApply)
        {
            
            _db.Entry(regApply).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag > 0 ? regApply.id : 0;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegApply> GetList()
        {
            return _db.RegApply.ToList();
        }

        public RegApply GetModel(int id)
        {
            RegApply regApply = _db.RegApply.Find(id);
            return regApply;
        }

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }

        public bool Update(RegApply regApply)
        {
            
            _db.Entry(regApply).State = System.Data.Entity.EntityState.Modified;
            int flag = _db.SaveChanges();
            if (flag > 0)
                return true;
            else
                return false;
        }
        public DbContextTransaction TransBegin()
        {
            _dbTransaction = _db.Database.BeginTransaction();
            return _dbTransaction;
        }
        public void Commit()
        {
            _dbTransaction.Commit();
        }
        public void RollBack()
        {
            _dbTransaction.Rollback();
        }
    }
}
