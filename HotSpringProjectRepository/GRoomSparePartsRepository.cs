using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class GRoomSparePartsRepository:IGRoomSparePartsRepository
    {
        private readonly HotSpringDbContext _Db;

        public GRoomSparePartsRepository(HotSpringDbContext hotSpringDbContext) 
        {
            _Db= hotSpringDbContext;
        }

        public bool Add(GRoomRepair gRoomRepair)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GRoomRepair> GetList()
        {
            throw new NotImplementedException();
        }

        public GRoomRepair GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(GRoomRepair gRoomRepair)
        {
            throw new NotImplementedException();
        }
    }
}
