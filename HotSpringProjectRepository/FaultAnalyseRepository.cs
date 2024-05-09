using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
namespace HotSpringProjectRepository
{
    public class FaultAnalyseRepository : IFaultAnalyseRepository
    {
        private readonly HotSpringDbContext _Db;

        public FaultAnalyseRepository(HotSpringDbContext hotSpringDbContext) { 
        _Db= hotSpringDbContext;
        }

        public int Add(FaultAnalyse faultAnalyse)
        {
            _Db.Entry<FaultAnalyse>(faultAnalyse).State=System.Data.Entity.EntityState.Added;
            int flag=_Db.SaveChanges();
            return flag;    
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FaultAnalyse> GetList()
        {
            throw new NotImplementedException();
        }

        public FaultAnalyse GetModel(int id)
        {
            return _Db.FaultAnalyse.Find(id);    
            
        }

        public IEnumerable<T> QueryBySql<T>(string sql)
        {

            return _Db.Database.SqlQuery<T>(sql);
        }

        public int UpDate(FaultAnalyse faultAnalyse)
        {
            throw new NotImplementedException();
        }

       
    }
}
