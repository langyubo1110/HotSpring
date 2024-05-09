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

        public bool Add(GRoomSpareParts gRoomSpareParts)
        {
            _Db.Entry(gRoomSpareParts).State = System.Data.Entity.EntityState.Added;
            int flag =_Db.SaveChanges();
            return flag>0?true:false;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GRoomSpareParts> GetList()
        {
            throw new NotImplementedException();
        }

        public GRoomSpareParts GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(GRoomSpareParts gRoomSpareParts)
        {
            throw new NotImplementedException();
        }
    }
}
