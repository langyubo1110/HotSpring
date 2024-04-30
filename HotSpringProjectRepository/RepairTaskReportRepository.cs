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
       
        public int add(RepaieTaskReport repaieTaskReport)
        {
            _Db.Entry<RepaieTaskReport>(repaieTaskReport).State=System.Data.Entity.EntityState.Added;
            int flag=_Db.SaveChanges();
            return flag;
        }

        public int delete(int id)
        {
            RepaieTaskReport repaieTaskReport=_Db.RepaieTaskReports.Find(id);
            _Db.Entry<RepaieTaskReport>(repaieTaskReport).State=System.Data.Entity.EntityState.Deleted;
            int flag=_Db.SaveChanges();
            return flag;
        }

        public IQueryable<RepaieTaskReport> getlist()
        {
          IQueryable<RepaieTaskReport> list =_Db.RepaieTaskReports;
            return list;
        }
       

        public RepaieTaskReport getmodel(int id)
        {
            return _Db.RepaieTaskReports.Find(id);
        }

        public int update(RepaieTaskReport repaieTaskReport)
        {
            _Db.Entry(repaieTaskReport).State=System.Data.Entity.EntityState.Modified;
            int flag=_Db.SaveChanges();
            return flag;
        }
        public IEnumerable<string> GetAllEquipmentNames()
        {
            IEnumerable<string> names = _Db.Equipment.Select(e => e.name);
            return names;
        }

    }
}
