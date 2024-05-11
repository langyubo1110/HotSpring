using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RepairRecordRepository : IRepairRecordRepository
    {
        private readonly HotSpringDbContext _Db;

        public RepairRecordRepository(HotSpringDbContext hotSpringDbContext)
        {
            _Db = hotSpringDbContext;
        }

        public int Add(RepairRecord repairRecord)
        {
            _Db.Entry<RepairRecord>(repairRecord).State = System.Data.Entity.EntityState.Added;
            int flag = _Db.SaveChanges();
            return flag;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RepairRecord> GetList()
        {
            throw new NotImplementedException();
        }

        public RepairRecord GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public int UpDate(RepairRecord repairRecord)
        {
            throw new NotImplementedException();
        }
    }
}
