using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
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

        public IEnumerable<FaultAnalyse> GetList()
        {
            IEnumerable<FaultAnalyse> list = _Db.FaultAnalyse;
            return list;
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
            _Db.Entry(faultAnalyse).State = System.Data.Entity.EntityState.Modified;
            int flag = _Db.SaveChanges();
            return flag;
        }

       
    }
}
