using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EquUpkeepTaskRepository : IEquUpkeepTaskRepository
    {
        private readonly HotSpringDbContext _db;

        public EquUpkeepTaskRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db=hotSpringDbContext;
        }
        public IEnumerable<EquUpkeepTask> GetList()
        {
            return _db.EquUpkeepTask;
        }
        IEnumerable<T> IEquUpkeepTaskRepository.QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }
        public int execBySql(string sql)
        {
            int flag = _db.Database.ExecuteSqlCommand(sql);
            return flag;
        }


    }
}
