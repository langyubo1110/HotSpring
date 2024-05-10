using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RegEquipResRepositpry : IRegEquipResRepositpry
    {
        private readonly HotSpringDbContext _db;

        public RegEquipResRepositpry(HotSpringDbContext HotSpringDbContext) {

            _db = HotSpringDbContext;
        }

        public int Add(RegEquipRes regEquipRes)
        {
            throw new NotImplementedException();
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

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public bool Update(RegEquipRes regEquipRes)
        {
            throw new NotImplementedException();
        }
    }
}
