using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class SystemPageCorrespondenceRepository : ISystemPageCorrespondenceRepository
    {
        private readonly HotSpringDbContext _db;

        public SystemPageCorrespondenceRepository(HotSpringDbContext keDa2024DbContext)
        {
            _db = keDa2024DbContext;
        }
        public int Add(SystemPageCorrespondence sp)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SystemPageCorrespondence> GetList()
        {
            return _db.SystemPageCorrespondences.ToList();
        }

        public SystemPageCorrespondence GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql).ToList();
        }

        public bool Update(SystemPageCorrespondence sp)
        {
            throw new NotImplementedException();
        }
    }
}
