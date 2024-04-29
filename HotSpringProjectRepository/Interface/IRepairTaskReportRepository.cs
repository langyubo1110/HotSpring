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
        IQueryable<RepaieTaskReport> getlist();
        int delete (int id);
        int update(RepaieTaskReport repaieTaskReport);

        int add(RepaieTaskReport repaieTaskReport);
        RepaieTaskReport getmodel(int id);
    }
}
