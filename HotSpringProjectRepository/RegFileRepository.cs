using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RegFileRepository : IRegFileRepository
    {
        private readonly HotSpringDbContext _db;

        public RegFileRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(RegFile regFile)
        {
            _db.Entry(regFile).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegFile> GetList()
        {
            return _db.RegFile;
        }
        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }
        public RegFile GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(RegFile regFile)
        {
            throw new NotImplementedException();
        }
    }
}
