using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
namespace HotSpringProjectRepository
{
    public class RepairTaskReportRepository:IRepairTaskReportRepository
    {
        private readonly HotSpringDbContext _Db;
        
        public RepairTaskReportRepository(HotSpringDbContext hotSpringDbContext) { 
        _Db = hotSpringDbContext;
        }
       
        public int Add(RepaieTaskReport repaieTaskReport)
        {
            _Db.Entry<RepaieTaskReport>(repaieTaskReport).State=System.Data.Entity.EntityState.Added;
            int flag=_Db.SaveChanges();
            return flag;
        }

        public int Delete(int id)
        {
            RepaieTaskReport repaieTaskReport=_Db.RepaieTaskReport.Find(id);
            _Db.Entry<RepaieTaskReport>(repaieTaskReport).State=System.Data.Entity.EntityState.Deleted;
            int flag=_Db.SaveChanges();
            return flag;
        }

        public IQueryable<RepaieTaskReport> GetList()
        {
          IQueryable<RepaieTaskReport> list =_Db.RepaieTaskReport;
            return list;
        }
       

        public RepaieTaskReport GetModel(int id)
        {
            return _Db.RepaieTaskReport.Find(id);
        }

        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _Db.Database.SqlQuery<T>(sql);
        }

        public int UpDate(RepaieTaskReport repaieTaskReport)
        {
            _Db.Entry(repaieTaskReport).State=System.Data.Entity.EntityState.Modified;
            int flag=_Db.SaveChanges();
            return flag;
        }
        

    }
}
