using HotSpringProject.Entity;

using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class RepoBuyRepository:IRepoBuyRepository
    {
        private readonly HotSpringDbContext _Db;

        public RepoBuyRepository(HotSpringDbContext hotSpringDb) 
        {
            _Db=hotSpringDb;
        }

        public bool Add(RepoBuy repoBuy)
        {
            _Db.Entry(repoBuy).State = System.Data.Entity.EntityState.Added;
            int flag = _Db.SaveChanges();
            return flag>0?true:false;
        }

        public int Delete(int id)
        {
            RepoBuy repoBuy =_Db.RepoBuy.Find(id);
            _Db.Entry(repoBuy).State=System.Data.Entity.EntityState.Deleted;
            int flag = _Db.SaveChanges();
            return flag;
        }

        public IEnumerable<RepoBuy> GetList()
        {
            IEnumerable<RepoBuy> list=_Db.RepoBuy;
            return list;
        }

        public RepoBuy GetModel(int id)
        {
            RepoBuy repoBuy = _Db.RepoBuy.Find(id);
            return repoBuy;
        }

        public bool Update(RepoBuy repoBuy)
        {
            _Db.Entry(repoBuy).State = System.Data.Entity.EntityState.Modified;
            int flag = _Db.SaveChanges();
            return flag>0?true:false;
        }
     
    }
}
