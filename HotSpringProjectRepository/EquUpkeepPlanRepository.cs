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

        int IEquUpkeepPlanRepository.Add(EquUpkeepPlan equUpkeepPlan)
        {
            _db.Entry(equUpkeepPlan).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        bool IEquUpkeepPlanRepository.Delete(int id)
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

        EquUpkeepPlan IEquUpkeepPlanRepository.GetModel(int id)
        {
            EquUpkeepPlan equUpkeepPlan = _db.EquUpkeepPlan.Find(id);
            return equUpkeepPlan;
        }

        IEnumerable<T> IEquUpkeepPlanRepository.QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }

        int IEquUpkeepPlanRepository.Update(EquUpkeepPlan equUpkeepPlan)
        {
            _db.Entry(equUpkeepPlan).State = System.Data.Entity.EntityState.Modified;
            int flag = _db.SaveChanges();
            return flag;
        }
    }
}
