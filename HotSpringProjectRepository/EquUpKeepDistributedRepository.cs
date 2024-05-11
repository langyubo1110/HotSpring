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
    public class EquUpKeepDistributedRepository : IEquUpKeepDistributedRepository
    {
        private readonly HotSpringDbContext _db;

        public EquUpKeepDistributedRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(EquUpKeepDistributed EquUpKeepDistributed)
        {
            _db.Entry(EquUpKeepDistributed).State = EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public IEnumerable<EquUpKeepDistributed> GetList()
        {
            return _db.EquUpKeepDistributed;
        }
    }
}
