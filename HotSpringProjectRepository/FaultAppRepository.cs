using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
namespace HotSpringProjectRepository
{
    public class FaultAppRepository : IFaultAppRepository
    {
        private readonly HotSpringDbContext _Db;

        public FaultAppRepository(HotSpringDbContext hotSpringDbContext)
        {
            _Db = hotSpringDbContext;
        }
        public int Add(FaultApp faultApp)
        {
            _Db.Entry<FaultApp>(faultApp).State = System.Data.Entity.EntityState.Added;
            int flag = _Db.SaveChanges();
            return flag;
        }

        public int Delete(int id)
        {
            FaultApp faultApp = _Db.FaultApp.Find(id);
            _Db.Entry<FaultApp>(faultApp).State = System.Data.Entity.EntityState.Deleted;
            int flag = _Db.SaveChanges();
            return flag;
        }

        public IQueryable<FaultApp> GetList()
        {
            IQueryable<FaultApp> list = _Db.FaultApp;
            return list;
        }


        public FaultApp GetModel(int id)
        {
            return _Db.FaultApp.Find(id);
        }

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _Db.Database.SqlQuery<T>(sql);
        }

        public int UpDate(FaultApp faultApp)
        {
            _Db.Entry(faultApp).State = System.Data.Entity.EntityState.Modified;
            int flag = _Db.SaveChanges();
            return flag;
        }
    }
}
