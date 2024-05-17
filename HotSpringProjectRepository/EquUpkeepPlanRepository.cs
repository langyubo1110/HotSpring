using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EquUpkeepPlanRepository : IEquUpkeepPlanRepository
    {
        private readonly HotSpringDbContext _db;

        public EquUpkeepPlanRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db=hotSpringDbContext;
        }
        public IEnumerable<EquUpkeepPlan> GetList()
        {
            return _db.EquUpkeepPlan;
        }

        public int Add(EquUpkeepPlan equUpkeepPlan)
        {
            _db.Entry(equUpkeepPlan).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public bool Delete(int id)
        {
            EquUpkeepPlan equUpkeepPlan = _db.EquUpkeepPlan.Find(id);
            if (equUpkeepPlan != null)
            {
                _db.Entry(equUpkeepPlan).State = System.Data.Entity.EntityState.Deleted;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public int execBySql(string sql)
        {
            int flag = _db.Database.ExecuteSqlCommand(sql);
            return flag;
        }

        public EquUpkeepPlan GetModel(int id)
        {
            EquUpkeepPlan equUpkeepPlan = _db.EquUpkeepPlan.Find(id);
            return equUpkeepPlan;
        }

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }

        public int Update(EquUpkeepPlan equUpkeepPlan)
        {
            _db.Entry(equUpkeepPlan).State = System.Data.Entity.EntityState.Modified;
            int flag = _db.SaveChanges();
            return flag;
        }
    }
}
