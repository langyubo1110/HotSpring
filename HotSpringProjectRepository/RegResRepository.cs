using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RegResRepository : IRegEquipResRepositpry
    {
        private readonly HotSpringDbContext _db;

        public RegResRepository(HotSpringDbContext HotSpringDbContext)
        {
            _db = HotSpringDbContext;
        }
        public int Add(RegEquipRes regEquipRes)
        {
            _db.Entry(regEquipRes).State = System.Data.Entity.EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegEquipRes> GetList()
        {
            return _db.RegEquipRes;
        }

        public RegEquipRes GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(RegEquipRes regEquipRes)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }
    }
}
