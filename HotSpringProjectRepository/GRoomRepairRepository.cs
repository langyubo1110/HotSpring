using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class GRoomRepairRepository:IGRoomRepairRepository
    {
        private readonly HotSpringDbContext _Db;

        public GRoomRepairRepository(HotSpringDbContext hotSpringDbContext) 
        {
            _Db= hotSpringDbContext;
        }
        //添加操作返回最新ID
        public int Add(GRoomRepair gRoomRepair)
        {
            _Db.Entry(gRoomRepair).State = System.Data.Entity.EntityState.Added;
            _Db.SaveChanges();
            return gRoomRepair.id;
        }

        public int Delete(int id)
        {
            GRoomRepair gRoomRepair = _Db.GRoomRepair.Find(id);
            _Db.Entry(gRoomRepair).State = System.Data.Entity.EntityState.Deleted;
            int flag = _Db.SaveChanges();
            return flag > 0 ? flag:0;
        }

        public IEnumerable<GRoomRepair> GetList()
        {
            IEnumerable<GRoomRepair> list =_Db.GRoomRepair;
            return list;
        }

        public GRoomRepair GetModel(int id)
        {
            GRoomRepair gRoomRepair = _Db.GRoomRepair.Find(id);
            return gRoomRepair;
        }

        public bool Update(GRoomRepair gRoomRepair)
        {
            _Db.Entry(gRoomRepair).State = System.Data.Entity.EntityState.Modified;
             int flag=_Db.SaveChanges();
            return flag>=0?true:false;
        }
    }
}
