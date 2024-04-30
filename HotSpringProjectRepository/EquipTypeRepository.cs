using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EquipTypeRepository : IEquipTypeRepository
    {
        private readonly HotSpringDbContext _db;

        //构造函数注入
        public EquipTypeRepository(HotSpringDbContext HotSpringDbContext)
        {
            _db = HotSpringDbContext;
        }

        public List<EquipType> GetList()
        {
            List<EquipType> list = _db.Equiptype.ToList();
            return list;
        }
    }
}
