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
        public IEnumerable<RegEquipRes> GetList()
        {
          return _db.RegEquipRes;
        }
    }
}
