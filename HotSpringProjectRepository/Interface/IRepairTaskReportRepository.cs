using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotSpringProject.Entity;
namespace HotSpringProjectRepository.Interface
{
    public interface IRepairTaskReportRepository
    {
        IQueryable<RepaieTaskReport> GetList();
        int Delete (int id);
        int UpDate(RepaieTaskReport repaieTaskReport);
        IEnumerable<T> QueryBySql<T>(string sql);
        int Add(RepaieTaskReport repaieTaskReport);
        
        RepaieTaskReport GetModel(int id);

    }
}
